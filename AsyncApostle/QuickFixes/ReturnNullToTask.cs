namespace AsyncApostle.QuickFixes;

[QuickFix]
public class ReturnNullAsTask : QuickFixBase
{
   #region fields

   readonly NullReturnAsTaskHighlighting _error;

   #endregion

   #region constructors

   public ReturnNullAsTask(NullReturnAsTaskHighlighting error) => _error = error;

   #endregion

   #region properties

   public override string Text => "Wrap to Task";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache) => _error.IsValid();

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      var factory  = GetInstance(_error.CSharpLiteralExpression);
      var taskType = CreateTypeByCLRName("System.Threading.Tasks.Task", _error.CSharpLiteralExpression.GetPsiModule());

      if (_error.ReturnType.IsTask())
         _error.CSharpLiteralExpression.ReplaceBy(factory.CreateReferenceExpression("$0.CompletedTask", taskType));
      else if (_error.ReturnType.IsGenericTask())
      {
         if (_error.ReturnType is not IDeclaredType declaredReturnType) return null;

         var substitution     = declaredReturnType.GetSubstitution();
         var genericParameter = substitution.Apply(substitution.Domain[0]);

         _error.CSharpLiteralExpression.ReplaceBy(factory.CreateExpression(genericParameter.IsStructType()
                                                                              ? "$0.FromResult(default($1))"
                                                                              : "$0.FromResult<$1>(null)",
                                                                           taskType,
                                                                           genericParameter));
      }

      return null;
   }

   #endregion
}