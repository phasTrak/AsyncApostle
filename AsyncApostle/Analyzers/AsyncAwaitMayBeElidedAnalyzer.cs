namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IParametersOwnerDeclaration), HighlightingTypes = [typeof(AsyncAwaitMayBeElidedHighlighting)])]
public class AsyncAwaitMayBeElidedAnalyzer : ElementProblemAnalyzer<IParametersOwnerDeclaration>
{
   #region methods

   protected override void Run(IParametersOwnerDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (!element.GetSolution()
                  .GetComponent<IAwaitEliderChecker>()
                  .CanElide(element))
         return;

      foreach (var awaitExpression in element.DescendantsInScope<IAwaitExpression>()) consumer.AddHighlighting(new AsyncAwaitMayBeElidedHighlighting(awaitExpression));
   }

   #endregion
}