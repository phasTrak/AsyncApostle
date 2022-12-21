namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              "Elide async/await",
                              "Elide async/await if task may be returned",
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class AsyncAwaitMayBeElidedHighlighting : IHighlighting
{
   #region fields

   public const string SeverityId = "AsyncApostle.AsyncAwaitMayBeElidedHighlighting";

   #endregion

   #region constructors

   public AsyncAwaitMayBeElidedHighlighting(IAwaitExpression awaitExpression) => AwaitExpression = awaitExpression;

   #endregion

   #region properties

   public IAwaitExpression AwaitExpression    { get; }
   public string           ErrorStripeToolTip => "Await may be elided.";
   public string           ToolTip            => "Async in method declaration and await may be elided.";

   #endregion

   #region methods

   public DocumentRange CalculateRange() => AwaitExpression.GetDocumentRange();
   public bool IsValid() => AwaitExpression.IsValid();

   #endregion
}