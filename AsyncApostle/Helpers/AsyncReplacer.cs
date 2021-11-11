using System.Linq;
using AsyncApostle.AsyncHelpers.AwaitEliders;
using AsyncApostle.Checkers.AsyncWait;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using static System.StringComparison;
using static JetBrains.ReSharper.Psi.TypeFactory;

namespace AsyncApostle.Helpers;

[SolutionComponent]
public class AsyncReplacer : IAsyncReplacer
{
    #region fields

    readonly IAsyncInvocationReplacer _asyncInvocationReplacer;
    readonly IAwaitElider _awaitElider;
    readonly IAwaitEliderChecker _awaitEliderChecker;
    readonly IInvocationConverter _invocationConverter;
    readonly ISyncWaitChecker _syncWaitChecker;
    readonly ISyncWaitConverter _syncWaitConverter;

    #endregion

    #region constructors

    public AsyncReplacer(IAsyncInvocationReplacer asyncInvocationReplacer,
                         IInvocationConverter invocationConverter,
                         IAwaitElider awaitElider,
                         IAwaitEliderChecker awaitEliderChecker,
                         ISyncWaitChecker syncWaitChecker,
                         ISyncWaitConverter syncWaitConverter)
    {
        _asyncInvocationReplacer = asyncInvocationReplacer;
        _awaitElider = awaitElider;
        _awaitEliderChecker = awaitEliderChecker;
        _invocationConverter = invocationConverter;
        _syncWaitChecker = syncWaitChecker;
        _syncWaitConverter = syncWaitConverter;
    }

    #endregion

    #region methods

    public void ReplaceToAsync(IMethod method)
    {
        foreach (var methodDeclaration in method.FindAllHierarchy()
                                                .SelectMany(x => x.GetDeclarations<IMethodDeclaration>()))
            ReplaceMethodToAsync(methodDeclaration);
    }

    static string GenerateAsyncMethodName(string oldName) =>
        oldName.EndsWith("Async", Ordinal)
            ? oldName
            : $"{oldName}Async";

    static void SetSignature(IMethodDeclaration methodDeclaration, IType newReturnValue, string newName)
    {
        methodDeclaration.SetType(newReturnValue);

        if (!methodDeclaration.IsAbstract)
            methodDeclaration.SetAsync(true);

        methodDeclaration.SetName(newName);
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

            if (task is null)
                return;

            newReturnValue = CreateType(task, returnType);
        }

        SetSignature(methodDeclaration, newReturnValue, GenerateAsyncMethodName(methodDeclaration.DeclaredName));

        if (_awaitEliderChecker.CanElide(methodDeclaration))
            _awaitElider.Elide(methodDeclaration);
    }

    void ReplaceMethodToAsync(IMethodDeclaration method)
    {
        if (!method.IsValid())
            return;

        var methodDeclaredElement = method.DeclaredElement;

        if (methodDeclaredElement is null)
            return;

        foreach (var invocation in method.GetPsiServices()
                                         .Finder.FindAllReferences(methodDeclaredElement)
                                         .Select(usage => usage.GetTreeNode()
                                                               .Parent as IInvocationExpression))
            _asyncInvocationReplacer.ReplaceInvocation(invocation, GenerateAsyncMethodName(method.DeclaredName), invocation?.IsUnderAsyncDeclaration() ?? false);

        // TODO: ugly hack. think
        IInvocationExpression? invocationExpression;

        while ((invocationExpression = method.DescendantsInScope<IInvocationExpression>()
                                             .FirstOrDefault(_syncWaitChecker.CanReplaceWaitToAsync)) is not null)
            _syncWaitConverter.ReplaceWaitToAsync(invocationExpression);

        IReferenceExpression? referenceExpression;

        while ((referenceExpression = method.DescendantsInScope<IReferenceExpression>()
                                            .FirstOrDefault(_syncWaitChecker.CanReplaceResultToAsync)) is not null)
            _syncWaitConverter.ReplaceResultToAsync(referenceExpression);

        while (method.DescendantsInScope<IInvocationExpression>()
                     .Any(invocationExpression2 => _invocationConverter.TryReplaceInvocationToAsync(invocationExpression2))) { }

        foreach (var parametersOwnerDeclaration in method.Descendants<IParametersOwnerDeclaration>()
                                                         .ToEnumerable()
                                                         .Where(_awaitEliderChecker.CanElide))
            _awaitElider.Elide(parametersOwnerDeclaration);

        ReplaceMethodSignatureToAsync(methodDeclaredElement, method);
    }

    #endregion
}