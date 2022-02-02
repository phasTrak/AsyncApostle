using AsyncApostle.AsyncHelpers.AwaitEliders;
using AsyncApostle.Helpers;
using AsyncApostle.Highlightings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IParametersOwnerDeclaration), HighlightingTypes = new[] { typeof(AsyncAwaitMayBeElidedHighlighting) })]
public class AsyncAwaitMayBeElidedAnalyzer : ElementProblemAnalyzer<IParametersOwnerDeclaration>
{
   #region methods

   protected override void Run(IParametersOwnerDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (!element.GetSolution()
                  .GetComponent<IAwaitEliderChecker>()
                  .CanElide(element))
         return;

      foreach (var awaitExpression in element.DescendantsInScope<IAwaitExpression>())
         consumer.AddHighlighting(new AsyncAwaitMayBeElidedHighlighting(awaitExpression));
   }

   #endregion
}