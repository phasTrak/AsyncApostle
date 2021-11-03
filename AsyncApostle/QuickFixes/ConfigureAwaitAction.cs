using System;
using AsyncApostle.Highlightings;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.TextControl;
using static JetBrains.ReSharper.Psi.CSharp.CSharpElementFactory;

namespace AsyncApostle.QuickFixes
{
    public class ConfigureAwaitAction : BulbActionBase
    {
        #region fields

        readonly ConfigureAwaitHighlighting _configureAwaitHighlighting;
        readonly bool _configureAwaitValue;

        #endregion

        #region constructors

        public ConfigureAwaitAction(ConfigureAwaitHighlighting configureAwaitHighlighting, bool configureAwaitValue) => (_configureAwaitHighlighting, _configureAwaitValue) = (configureAwaitHighlighting, configureAwaitValue);

        #endregion

        #region properties

        public override string Text => $"Add ConfigureAwait({GetConfigureAwaitValueText()})";

        #endregion

        #region methods

        protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var awaitExpression = _configureAwaitHighlighting.AwaitExpression;

            awaitExpression.Task.ReplaceBy(GetInstance(awaitExpression)
                                               .CreateExpression($"$0.ConfigureAwait({GetConfigureAwaitValueText()})", awaitExpression.Task));

            return null;
        }

        string GetConfigureAwaitValueText() =>
            _configureAwaitValue
                ? "true"
                : "false";

        #endregion
    }
}
