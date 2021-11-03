using AsyncApostle.AsyncHelpers.MethodFinders;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers
{
    [SolutionComponent]
    class HaveAsyncMethodChecker : IConcreteCanBeUseAsyncMethodChecker
    {
        #region fields

        readonly IAsyncMethodFinder _asyncMethodFinder;

        #endregion

        #region constructors

        public HaveAsyncMethodChecker(IAsyncMethodFinder asyncMethodFinder) => _asyncMethodFinder = asyncMethodFinder;

        #endregion

        #region methods

        public bool CanReplace(IInvocationExpression element)
        {
            var referenceCurrentResolveResult = element.Reference.Resolve();

            return referenceCurrentResolveResult.IsValid()
                   && referenceCurrentResolveResult.DeclaredElement is IMethod invocationMethod
                   && _asyncMethodFinder.FindEquivalentAsyncMethod(invocationMethod, (element.ConditionalQualifier as IReferenceExpression)?.QualifierExpression?.Type()!)
                                        .ParameterCompareResult.CanBeConvertedToAsync();
        }

        #endregion
    }
}
