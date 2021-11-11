using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static AsyncApostle.Settings.AsyncApostleGroupSettings;
using static JetBrains.ReSharper.Feature.Services.Daemon.Severity;

namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId, null, Id, "Await not configured", "If await not configured it may cause deadlock", WARNING)]
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

    public IAwaitExpression AwaitExpression { get; }
    public string ErrorStripeToolTip => "Await not configured";
    public string ToolTip => "If await not configured it may cause deadlock, if this code will be call synchronously";

    #endregion

    #region methods

    public DocumentRange CalculateRange() => AwaitExpression.GetDocumentRange();
    public bool IsValid() => AwaitExpression.IsValid();

    #endregion
}