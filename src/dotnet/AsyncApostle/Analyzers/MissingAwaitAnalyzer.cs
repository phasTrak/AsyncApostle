using System.Linq;
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
      var returnStatement = element.DescendantsInScope<IReturnStatement>().FirstOrDefault();

      foreach (var invocationExpression in element.DescendantsInScope<IInvocationExpression>())
         if (missingAwaitChecker.AwaitIsMissing(invocationExpression, returnStatement))
            consumer.AddHighlighting(new MissingAwaitHighlighting(invocationExpression));
   }

   #endregion
}