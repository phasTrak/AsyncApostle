namespace AsyncApostle.QuickFixes;

[QuickFix]
public class ConfigureAwaitQuickFix(ConfigureAwaitHighlighting configureAwaitHighlighting) : IQuickFix
{
   #region methods

   public IEnumerable<IntentionAction> CreateBulbItems() =>
      new[]
      {
         new ConfigureAwaitAction(configureAwaitHighlighting, false),
         new ConfigureAwaitAction(configureAwaitHighlighting, true)
      }.ToQuickFixIntentions();

   public bool IsAvailable(IUserDataHolder cache) => configureAwaitHighlighting.IsValid();

   #endregion
}