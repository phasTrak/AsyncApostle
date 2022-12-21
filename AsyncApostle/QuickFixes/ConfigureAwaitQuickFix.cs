namespace AsyncApostle.QuickFixes;

[QuickFix]
public class ConfigureAwaitQuickFix : IQuickFix
{
   #region fields

   readonly ConfigureAwaitHighlighting _configureAwaitHighlighting;

   #endregion

   #region constructors

   public ConfigureAwaitQuickFix(ConfigureAwaitHighlighting configureAwaitHighlighting) => _configureAwaitHighlighting = configureAwaitHighlighting;

   #endregion

   #region methods

   public IEnumerable<IntentionAction> CreateBulbItems() =>
      new[]
      {
         new ConfigureAwaitAction(_configureAwaitHighlighting, false),
         new ConfigureAwaitAction(_configureAwaitHighlighting, true)
      }.ToQuickFixIntentions();

   public bool IsAvailable(IUserDataHolder cache) => _configureAwaitHighlighting.IsValid();

   #endregion
}