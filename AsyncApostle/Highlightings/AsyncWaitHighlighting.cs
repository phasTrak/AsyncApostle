using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static AsyncApostle.Settings.AsyncApostleGroupSettings;
using static JetBrains.ReSharper.Feature.Services.Daemon.Severity;

namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId, null, Id, "Use async wait instead sync wait.", "Use async wait instead sync wait.", ERROR)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class AsyncWaitHighlighting : IHighlighting
{
    #region fields

    public const string SeverityId = "AsyncApostle.AsyncWait";

    #endregion

    #region constructors

    public AsyncWaitHighlighting(IReferenceExpression referenceExpression) => ReferenceExpression = referenceExpression;
    public AsyncWaitHighlighting(IInvocationExpression invocationExpression) => InvocationExpression = invocationExpression;

    #endregion

    #region properties

    public string ErrorStripeToolTip => "Use async wait.";
    public IInvocationExpression? InvocationExpression { get; }
    public IReferenceExpression? ReferenceExpression { get; }
    public string ToolTip => "Use async wait instead sync wait.";

    #endregion

    #region methods

    public DocumentRange CalculateRange() => ReferenceExpression?.GetDocumentRange() ?? InvocationExpression.GetDocumentRange();
    public bool IsValid() => ReferenceExpression?.IsValid() ?? InvocationExpression?.IsValid() ?? false;

    #endregion
}