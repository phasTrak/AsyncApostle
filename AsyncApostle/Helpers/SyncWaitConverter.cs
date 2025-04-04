namespace AsyncApostle.Helpers;

[SolutionComponent(DemandAnyThreadSafe)]
public class SyncWaitConverter : ISyncWaitConverter
{
   #region methods

   static void ReplaceToAwait(ICSharpExpression invocationExpression, ITreeNode replaceBy) =>
      invocationExpression.ReplaceBy(GetInstance(invocationExpression)
                                       .CreateExpression("await $0.ConfigureAwait(false)", replaceBy));

   public void ReplaceResultToAsync(IReferenceExpression referenceExpression)
   {
      var replaceBy = referenceExpression.QualifierExpression;

      if (replaceBy is not null) ReplaceToAwait(referenceExpression, replaceBy);
   }

   public void ReplaceWaitToAsync(IInvocationExpression invocationExpression)
   {
      var replaceBy = invocationExpression.FirstChild?.FirstChild;

      if (replaceBy is not null) ReplaceToAwait(invocationExpression, replaceBy);
   }

   #endregion
}