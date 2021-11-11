using System.Collections.Generic;
using System.Linq;
using AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers.CustomCheckers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers;

[SolutionComponent]
class ConfigureAwaitChecker : IConfigureAwaitChecker
{
    #region fields

    readonly IConfigureAwaitCustomChecker[] _awaitCustomCheckers;

    #endregion

    #region constructors

    public ConfigureAwaitChecker(IEnumerable<IConfigureAwaitCustomChecker> awaitCustomCheckers) => _awaitCustomCheckers = awaitCustomCheckers.ToArray();

    #endregion

    #region methods

    public bool NeedAdding(IAwaitExpression element) => _awaitCustomCheckers.All(x => x.CanBeAdded(element));

    #endregion
}