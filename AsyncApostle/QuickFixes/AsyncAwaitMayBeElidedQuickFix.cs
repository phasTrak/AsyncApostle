namespace AsyncApostle.QuickFixes;

[QuickFix]
public class AsyncAwaitMayBeElidedQuickFix(AsyncAwaitMayBeElidedHighlighting asyncAwaitMayBeElidedHighlighting) : QuickFixBase
{
   #region properties

   public override string Text => "Remove async/await.";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache) => asyncAwaitMayBeElidedHighlighting.IsValid();

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      solution.GetComponent<IAwaitElider>()
              .Elide(asyncAwaitMayBeElidedHighlighting.AwaitExpression);

      return null;
   }

   #endregion
}