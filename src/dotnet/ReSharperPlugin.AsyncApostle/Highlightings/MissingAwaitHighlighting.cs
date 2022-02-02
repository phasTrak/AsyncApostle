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
    Message,
    Message,
    WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class MissingAwaitHighlighting : IHighlighting
{
    #region fields

    private const string SeverityId = "AsyncApostle.MissingAwaitHighlighting";
    private const string Message = "Return or await Task";

    #endregion

    #region constructors

    public MissingAwaitHighlighting(IInvocationExpression invocationExpression) =>
        InvocationExpression = invocationExpression;

    #endregion

    #region properties

    public string ErrorStripeToolTip => Message;
    public string ToolTip => Message;
    private ICSharpExpression InvocationExpression { get; }

    #endregion

    #region methods

    public DocumentRange CalculateRange() => InvocationExpression.GetDocumentRange();
    public bool IsValid() => InvocationExpression.IsValid();

    #endregion
}