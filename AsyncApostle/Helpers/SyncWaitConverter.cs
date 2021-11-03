using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using static JetBrains.ReSharper.Psi.CSharp.CSharpElementFactory;

namespace AsyncApostle.Helpers
{
    [SolutionComponent]
    public class SyncWaitConverter : ISyncWaitConverter
    {
        #region methods

        public void ReplaceResultToAsync(IReferenceExpression referenceExpression)
        {
            var replaceBy = referenceExpression.QualifierExpression;

            if (replaceBy is not null)
                ReplaceToAwait(referenceExpression, replaceBy);
        }

        public void ReplaceWaitToAsync(IInvocationExpression invocationExpression)
        {
            var replaceBy = invocationExpression.FirstChild?.FirstChild;

            if (replaceBy is not null)
                ReplaceToAwait(invocationExpression, replaceBy);
        }

        static void ReplaceToAwait(ICSharpExpression invocationExpression, ITreeNode replaceBy) =>
            invocationExpression.ReplaceBy(GetInstance(invocationExpression)
                                               .CreateExpression("await $0.ConfigureAwait(false)", replaceBy));

        #endregion
    }
}
