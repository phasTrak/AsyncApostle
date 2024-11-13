namespace AsyncApostle.Helpers;

[SolutionComponent(DemandAnyThreadSafe)]
public class AsyncReplacer(IAsyncInvocationReplacer asyncInvocationReplacer,
                           IInvocationConverter invocationConverter,
                           IAwaitElider awaitElider,
                           IAwaitEliderChecker awaitEliderChecker,
                           ISyncWaitChecker syncWaitChecker,
                           ISyncWaitConverter syncWaitConverter) : IAsyncReplacer
{
   #region methods

   static string GenerateAsyncMethodName(string oldName) =>
      oldName.EndsWith("Async", Ordinal)
         ? oldName
         : $"{oldName}Async";

   static void SetSignature(IMethodDeclaration methodDeclaration, IType newReturnValue, string newName)
   {
      methodDeclaration.SetType(newReturnValue);

      if (!methodDeclaration.IsAbstract) methodDeclaration.SetAsync(true);

      methodDeclaration.SetName(newName);
   }

   public void ReplaceToAsync(IMethod method)
   {
      foreach (var methodDeclaration in method.FindAllHierarchy()
                                              .SelectMany(static x => x.GetDeclarations<IMethodDeclaration>()))
         ReplaceMethodToAsync(methodDeclaration);
   }

   void ReplaceMethodSignatureToAsync(IParametersOwner parametersOwner, IMethodDeclaration methodDeclaration)
   {
      var returnType = parametersOwner.ReturnType;

      var psiModule = methodDeclaration.GetPsiModule();

      IDeclaredType newReturnValue;

      if (returnType.IsVoid())
         newReturnValue = CreateTypeByCLRName("System.Threading.Tasks.Task", psiModule);
      else
      {
         var task = CreateTypeByCLRName("System.Threading.Tasks.Task`1", psiModule)
           .GetTypeElement();

         if (task is null) return;

         newReturnValue = CreateType(task, [returnType]);
      }

      SetSignature(methodDeclaration, newReturnValue, GenerateAsyncMethodName(methodDeclaration.DeclaredName));

      if (awaitEliderChecker.CanElide(methodDeclaration)) awaitElider.Elide(methodDeclaration);
   }

   void ReplaceMethodToAsync(IMethodDeclaration method)
   {
      if (!method.IsValid()) return;

      var methodDeclaredElement = method.DeclaredElement;

      if (methodDeclaredElement is null) return;

      foreach (var invocation in method.GetPsiServices()
                                       .Finder.FindAllReferences(methodDeclaredElement)
                                       .Select(static usage => usage.GetTreeNode()
                                                                    .Parent as IInvocationExpression))
         asyncInvocationReplacer.ReplaceInvocation(invocation, GenerateAsyncMethodName(method.DeclaredName), invocation?.IsUnderAsyncDeclaration() is true);

      // TODO: ugly hack. think
      while (method.DescendantsInScope<IInvocationExpression>()
                   .FirstOrDefault(syncWaitChecker.CanReplaceWaitToAsync) is { } invocationExpression)
         syncWaitConverter.ReplaceWaitToAsync(invocationExpression);

      while (method.DescendantsInScope<IReferenceExpression>()
                   .FirstOrDefault(syncWaitChecker.CanReplaceResultToAsync) is { } referenceExpression)
         syncWaitConverter.ReplaceResultToAsync(referenceExpression);

      while (method.DescendantsInScope<IInvocationExpression>()
                   .Any(invocationConverter.TryReplaceInvocationToAsync))
      {
         // block intentionally left empty
      }

      foreach (var parametersOwnerDeclaration in method.Descendants<IParametersOwnerDeclaration>()
                                                       .ToEnumerable()
                                                       .Where(awaitEliderChecker.CanElide))
         awaitElider.Elide(parametersOwnerDeclaration);

      ReplaceMethodSignatureToAsync(methodDeclaredElement, method);
   }

   #endregion
}