namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              "Await not configured",
                              "If await not configured it may cause deadlock",
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class ConfigureAwaitHighlighting : IHighlighting
{
   #region fields

   public const string SeverityId = "AsyncApostle.ConfigureAwaitHighlighting";

   #endregion

   #region constructors

   public ConfigureAwaitHighlighting(IAwaitExpression awaitExpression) => AwaitExpression = awaitExpression;

   #endregion

   #region properties

   public IAwaitExpression AwaitExpression    { get; }
   public string           ErrorStripeToolTip => "Await not configured";
   public string           ToolTip            => "If await not configured it may cause deadlock, if this code will be call synchronously";

   #endregion

   #region methods

   public DocumentRange CalculateRange() => AwaitExpression.GetDocumentRange();
   public bool IsValid() => AwaitExpression.IsValid();

   #endregion
}