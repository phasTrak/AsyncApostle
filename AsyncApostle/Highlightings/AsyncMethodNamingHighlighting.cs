namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              """
                              Async method must end with "Async"
                              """,
                              """
                              Async method must end with "Async"
                              """,
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class AsyncMethodNamingHighlighting(IMethodDeclaration methodDeclaration) : IHighlighting
{
   public const string SeverityId = "AsyncApostle.AsyncMethodNamingHighlighting";

   public string ErrorStripeToolTip =>
      """
      Async method must end with "Async"
      """;

   public IMethodDeclaration MethodDeclaration { get; } = methodDeclaration;

   public string ToolTip =>
      """
      Async method must end with "Async"
      """;

   public DocumentRange CalculateRange() => MethodDeclaration.NameIdentifier.GetDocumentRange();
   public bool IsValid() => MethodDeclaration.IsValid();
}