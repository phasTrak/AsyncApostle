namespace AsyncApostle.QuickFixes;

[QuickFix]
public class AsyncWaitQuickFix(AsyncWaitHighlighting asyncWaitHighlighting) : QuickFixBase
{
   #region properties

   public override string Text => "Use await";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache) => asyncWaitHighlighting.IsValid();

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      var syncWaitConverter = solution.GetComponent<ISyncWaitConverter>();

      if (asyncWaitHighlighting.InvocationExpression is not null) syncWaitConverter.ReplaceWaitToAsync(asyncWaitHighlighting.InvocationExpression);

      if (asyncWaitHighlighting.ReferenceExpression is not null) syncWaitConverter.ReplaceResultToAsync(asyncWaitHighlighting.ReferenceExpression);

      return null;
   }

   #endregion
}