using System;
using AsyncApostle.Helpers;
using AsyncApostle.Highlightings;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AsyncApostle.QuickFixes
{
    [QuickFix]
    public class AsyncWaitQuickFix : QuickFixBase
    {
        #region fields

        readonly AsyncWaitHighlighting _asyncWaitHighlighting;

        #endregion

        #region constructors

        public AsyncWaitQuickFix(AsyncWaitHighlighting asyncWaitHighlighting) => _asyncWaitHighlighting = asyncWaitHighlighting;

        #endregion

        #region properties

        public override string Text => "Use await";

        #endregion

        #region methods

        public override bool IsAvailable(IUserDataHolder cache) => _asyncWaitHighlighting.IsValid();

        protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var syncWaitConverter = solution.GetComponent<ISyncWaitConverter>();

            if (_asyncWaitHighlighting.InvocationExpression is not null)
                syncWaitConverter.ReplaceWaitToAsync(_asyncWaitHighlighting.InvocationExpression);

            if (_asyncWaitHighlighting.ReferenceExpression is not null)
                syncWaitConverter.ReplaceResultToAsync(_asyncWaitHighlighting.ReferenceExpression);

            return null;
        }

        #endregion
    }
}
