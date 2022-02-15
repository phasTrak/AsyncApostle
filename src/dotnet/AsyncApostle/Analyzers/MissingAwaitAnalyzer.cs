using AsyncApostle.AsyncHelpers.MissingAwaitChecker;
using AsyncApostle.Helpers;
using AsyncApostle.Highlightings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IParametersOwnerDeclaration),
   HighlightingTypes = new[] { typeof(MissingAwaitHighlighting) })]
public class MissingAwaitAnalyzer : ElementProblemAnalyzer<IParametersOwnerDeclaration>
{
   #region methods

   protected override void Run(IParametersOwnerDeclaration element, ElementProblemAnalyzerData data,
      IHighlightingConsumer consumer)
   {
      var missingAwaitChecker = element.GetSolution().GetComponent<IMissingAwaitChecker>();

      foreach (var cSharpTreeNode in element.DescendantsInScope<ICSharpTreeNode>())
         if (missingAwaitChecker.AwaitIsMissing(cSharpTreeNode))
            consumer.AddHighlighting(
               new MissingAwaitHighlighting(cSharpTreeNode.GetContainingNode<IInvocationExpression>()!));
   }

   #endregion
}