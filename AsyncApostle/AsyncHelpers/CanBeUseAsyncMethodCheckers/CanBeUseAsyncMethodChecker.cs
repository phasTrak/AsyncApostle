using System.Collections.Generic;
using System.Linq;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers.CanBeUseAsyncMethodCheckers;

[SolutionComponent]
class CanBeUseAsyncMethodChecker : ICanBeUseAsyncMethodChecker
{
   #region fields

   readonly IConcreteCanBeUseAsyncMethodChecker[] _checkers;

   #endregion

   #region constructors

   public CanBeUseAsyncMethodChecker(IEnumerable<IConcreteCanBeUseAsyncMethodChecker> checkers) => _checkers = checkers.ToArray();

   #endregion

   #region methods

   public bool CanReplace(IInvocationExpression element) => _checkers.All(x => x.CanReplace(element));

   #endregion
}