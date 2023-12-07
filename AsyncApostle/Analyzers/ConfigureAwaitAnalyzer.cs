namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IAwaitExpression), HighlightingTypes = [typeof(ConfigureAwaitHighlighting)])]
public class ConfigureAwaitAnalyzer : ElementProblemAnalyzer<IAwaitExpression>
{
   #region methods

   protected override void Run(IAwaitExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (!element.GetSolution()
                  .GetComponent<IConfigureAwaitChecker>()
                  .NeedAdding(element))
         return;

      consumer.AddHighlighting(new ConfigureAwaitHighlighting(element));
   }

   #endregion
}