using AsyncApostle.AsyncHelpers.Checker;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers
{
    [SolutionComponent]
    class AttributeFunctionChecker : IConfigureAwaitCustomChecker
    {
        #region fields

        readonly IAttributeFunctionChecker _attributeFunctionChecker;

        #endregion

        #region constructors

        public AttributeFunctionChecker(IAttributeFunctionChecker attributeFunctionChecker) => _attributeFunctionChecker = attributeFunctionChecker;

        #endregion

        #region methods

        public bool CanBeAdded(IAwaitExpression element) => !_attributeFunctionChecker.IsUnder(element);

        #endregion
    }
}
