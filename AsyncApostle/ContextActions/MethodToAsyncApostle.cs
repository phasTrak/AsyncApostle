namespace AsyncApostle.ContextActions;

[ContextAction(Group = "C#", Name = "ConvertToAsync", Description = "Convert method to async and replace all inner call to async version if exist.")]
public class MethodToAsyncApostle(ICSharpContextActionDataProvider provider) : ContextActionBase
{
   #region fields

   IAsyncReplacer? _asyncReplacer;

   #endregion

   #region properties

   public override string           Text     => "Convert method to async and replace all inner call to async version if exist.";
   ICSharpContextActionDataProvider Provider { get; } = provider;

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache)
   {
      var method = GetMethodFromCaretPosition();

      if (method is null) return false;

      var returnType = method.DeclaredElement?.ReturnType;

      return !(returnType?.IsTask() is true || returnType?.IsGenericTask() is true);
   }

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      _asyncReplacer = solution.GetComponent<IAsyncReplacer>();

      var methodDeclaredElement = GetMethodFromCaretPosition()
       ?.DeclaredElement;

      if (methodDeclaredElement is null) return null;

      _asyncReplacer.ReplaceToAsync(methodDeclaredElement);

      return null;
   }

   IMethodDeclaration? GetMethodFromCaretPosition() => (Provider.TokenAfterCaret as ICSharpIdentifier ?? Provider.TokenBeforeCaret as ICSharpIdentifier)?.Parent as IMethodDeclaration;

   #endregion
}