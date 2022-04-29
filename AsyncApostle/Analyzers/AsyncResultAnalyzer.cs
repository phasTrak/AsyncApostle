using AsyncApostle.Checkers.AsyncWait;
using AsyncApostle.Highlightings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IInvocationExpression), HighlightingTypes = new[] { typeof(AsyncWaitHighlighting) })]
public class AsyncResultAnalyzer : ElementProblemAnalyzer<IInvocationExpression>
{
   #region fields

   readonly ISyncWaitChecker _syncWaitChecker;

   #endregion

   #region constructors

   public AsyncResultAnalyzer(ISyncWaitChecker syncWaitChecker) => _syncWaitChecker = syncWaitChecker;

   #endregion

   #region methods

   protected override void Run(IInvocationExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (_syncWaitChecker.CanReplaceWaitToAsync(element)) consumer.AddHighlighting(new AsyncWaitHighlighting(element));
   }

   #endregion
}