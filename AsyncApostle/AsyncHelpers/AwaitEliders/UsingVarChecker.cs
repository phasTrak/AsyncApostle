using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static JetBrains.ReSharper.Psi.CSharp.Tree.UsingDeclarationKind;

namespace AsyncApostle.AsyncHelpers.AwaitEliders
{
    [SolutionComponent]
    class UsingVarChecker : IConcreteAwaitEliderChecker
    {
        #region methods

        public bool CanElide(IParametersOwnerDeclaration element) =>
            element.Descendants<IMultipleLocalVariableDeclaration>()
                   .ToEnumerable()
                   .All(x => x.UsingKind is Regular);

        #endregion
    }
}
