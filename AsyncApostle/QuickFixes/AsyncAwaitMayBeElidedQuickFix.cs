namespace AsyncApostle.QuickFixes;

[QuickFix]
public class AsyncAwaitMayBeElidedQuickFix : QuickFixBase
{
   #region fields

   readonly AsyncAwaitMayBeElidedHighlighting _asyncAwaitMayBeElidedHighlighting;

   #endregion

   #region constructors

   public AsyncAwaitMayBeElidedQuickFix(AsyncAwaitMayBeElidedHighlighting asyncAwaitMayBeElidedHighlighting) => _asyncAwaitMayBeElidedHighlighting = asyncAwaitMayBeElidedHighlighting;

   #endregion

   #region properties

   public override string Text => "Remove async/await.";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache) => _asyncAwaitMayBeElidedHighlighting.IsValid();

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      solution.GetComponent<IAwaitElider>()
              .Elide(_asyncAwaitMayBeElidedHighlighting.AwaitExpression);

      return null;
   }

   #endregion
}