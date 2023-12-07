namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IInvocationExpression), HighlightingTypes = [typeof(AsyncWaitHighlighting)])]
public class AsyncResultAnalyzer(ISyncWaitChecker syncWaitChecker) : ElementProblemAnalyzer<IInvocationExpression>
{
   #region methods

   protected override void Run(IInvocationExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (syncWaitChecker.CanReplaceWaitToAsync(element)) consumer.AddHighlighting(new AsyncWaitHighlighting(element));
   }

   #endregion
}