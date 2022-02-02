using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.Checkers.AsyncWait;

[SolutionComponent]
class SyncWaitChecker : ISyncWaitChecker
{
   #region methods

   static bool IsResult(IReferenceExpression referenceExpression) => referenceExpression.NameIdentifier?.Name is "Result";

   static IType? ResolveTargetType(IInvocationExpression invocationExpression) =>
      (invocationExpression.InvokedExpression.FirstChild as IReferenceExpression)?.Type() ?? (invocationExpression.InvokedExpression.FirstChild as IInvocationExpression)?.Type();

   public bool CanReplaceResultToAsync(IReferenceExpression referenceExpression)
   {
      var type = referenceExpression.QualifierExpression?.Type();

      return (type?.IsGenericTask() is true || type?.IsTask() is true) && IsResult(referenceExpression);
   }

   public bool CanReplaceWaitToAsync(IInvocationExpression invocationExpression)
   {
      var targetType = ResolveTargetType(invocationExpression);

      return invocationExpression.Reference.Resolve()
                                 .Result.DeclaredElement?.ShortName is "Wait" or "AwaitResult"
          && (targetType?.IsTask() is true || targetType?.IsGenericTask() is true);
   }

   #endregion
}