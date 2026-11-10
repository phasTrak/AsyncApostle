namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              "Use async wait instead sync wait.",
                              "Use async wait instead sync wait.",
                              ERROR)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class AsyncWaitHighlighting : IHighlighting
{
   public const string SeverityId = "AsyncApostle.AsyncWait";
   public AsyncWaitHighlighting(IReferenceExpression referenceExpression) => ReferenceExpression = referenceExpression;
   public AsyncWaitHighlighting(IInvocationExpression invocationExpression) => InvocationExpression = invocationExpression;
   public string                 ErrorStripeToolTip   => "Use async wait.";
   public IInvocationExpression? InvocationExpression { get; }
   public IReferenceExpression?  ReferenceExpression  { get; }
   public string                 ToolTip              => "Use async wait instead sync wait.";
   public DocumentRange CalculateRange() => ReferenceExpression?.GetDocumentRange() ?? InvocationExpression.GetDocumentRange();
   public bool IsValid() => ReferenceExpression?.IsValid()                          ?? InvocationExpression?.IsValid() is true;
}