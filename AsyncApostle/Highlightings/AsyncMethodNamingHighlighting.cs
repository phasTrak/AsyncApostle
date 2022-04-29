using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static AsyncApostle.Settings.AsyncApostleGroupSettings;
using static JetBrains.ReSharper.Feature.Services.Daemon.Severity;

namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              @"Async method must end with ""Async""",
                              @"Async method must end with ""Async""",
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class AsyncMethodNamingHighlighting : IHighlighting
{
   #region fields

   public const string SeverityId = "AsyncApostle.AsyncMethodNamingHighlighting";

   #endregion

   #region constructors

   public AsyncMethodNamingHighlighting(IMethodDeclaration methodDeclaration) => MethodDeclaration = methodDeclaration;

   #endregion

   #region properties

   public string             ErrorStripeToolTip => @"Async method must end with ""Async""";
   public IMethodDeclaration MethodDeclaration  { get; }
   public string             ToolTip            => @"Async method must end with ""Async""";

   #endregion

   #region methods

   public DocumentRange CalculateRange() => MethodDeclaration.NameIdentifier.GetDocumentRange();
   public bool IsValid() => MethodDeclaration.IsValid();

   #endregion
}