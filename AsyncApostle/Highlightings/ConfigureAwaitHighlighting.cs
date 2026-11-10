namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              "Await not configured",
                              "If await not configured it may cause deadlock",
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class ConfigureAwaitHighlighting(IAwaitExpression awaitExpression) : IHighlighting
{
   public const string           SeverityId = "AsyncApostle.ConfigureAwaitHighlighting";
   public       IAwaitExpression AwaitExpression    { get; } = awaitExpression;
   public       string           ErrorStripeToolTip => "Await not configured";
   public       string           ToolTip            => "If await not configured it may cause deadlock, if this code will be call synchronously";
   public DocumentRange CalculateRange() => AwaitExpression.GetDocumentRange();
   public bool IsValid() => AwaitExpression.IsValid();
}