using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static AsyncApostle.Settings.AsyncApostleGroupSettings;
using static JetBrains.ReSharper.Feature.Services.Daemon.Severity;

namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId, null, Id, "Elide async/await", "Elide async/await if task may be returned", WARNING)]
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

    public IAwaitExpression AwaitExpression { get; }
    public string ErrorStripeToolTip => "Await may be elided.";
    public string ToolTip => "Async in method declaration and await may be elided.";

    #endregion

    #region methods

    public DocumentRange CalculateRange() => AwaitExpression.GetDocumentRange();
    public bool IsValid() => AwaitExpression.IsValid();

    #endregion
}