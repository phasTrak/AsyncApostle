using AsyncApostle.Checkers.AsyncWait;
using AsyncApostle.Highlightings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IReferenceExpression), HighlightingTypes = new[] { typeof(AsyncWaitHighlighting) })]
public class AsyncWaitAnalyzer : ElementProblemAnalyzer<IReferenceExpression>
{
   #region fields

   readonly ISyncWaitChecker _syncWaitChecker;

   #endregion

   #region constructors

   public AsyncWaitAnalyzer(ISyncWaitChecker syncWaitChecker) => _syncWaitChecker = syncWaitChecker;

   #endregion

   #region methods

   protected override void Run(IReferenceExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (_syncWaitChecker.CanReplaceResultToAsync(element))
         consumer.AddHighlighting(new AsyncWaitHighlighting(element));
   }

   #endregion
}