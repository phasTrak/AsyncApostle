namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              "Elide async/await",
                              "Elide async/await if task may be returned",
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class AsyncAwaitMayBeElidedHighlighting(IAwaitExpression awaitExpression) : IHighlighting
{
   public const string           SeverityId = "AsyncApostle.AsyncAwaitMayBeElidedHighlighting";
   public       IAwaitExpression AwaitExpression    { get; } = awaitExpression;
   public       string           ErrorStripeToolTip => "Await may be elided.";
   public       string           ToolTip            => "Async in method declaration and await may be elided.";
   public DocumentRange CalculateRange() => AwaitExpression.GetDocumentRange();
   public bool IsValid() => AwaitExpression.IsValid();
}