namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IReferenceExpression), HighlightingTypes = [typeof(AsyncWaitHighlighting)])]
public class AsyncWaitAnalyzer(ISyncWaitChecker syncWaitChecker) : ElementProblemAnalyzer<IReferenceExpression>
{
   #region methods

   protected override void Run(IReferenceExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (syncWaitChecker.CanReplaceResultToAsync(element)) consumer.AddHighlighting(new AsyncWaitHighlighting(element));
   }

   #endregion
}