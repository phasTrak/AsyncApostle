namespace AsyncApostle.QuickFixes;

[QuickFix]
public class ReturnNullAsTask(NullReturnAsTaskHighlighting error) : QuickFixBase
{
   #region properties

   public override string Text => "Wrap to Task";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache) => error.IsValid();

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      var factory  = GetInstance(error.CSharpLiteralExpression);
      var taskType = CreateTypeByCLRName("System.Threading.Tasks.Task", error.CSharpLiteralExpression.GetPsiModule());

      if (error.ReturnType.IsTask())
         error.CSharpLiteralExpression.ReplaceBy(factory.CreateReferenceExpression("$0.CompletedTask", taskType));
      else if (error.ReturnType.IsGenericTask())
      {
         if (error.ReturnType is not IDeclaredType declaredReturnType) return null;

         var substitution     = declaredReturnType.GetSubstitution();
         var genericParameter = substitution.Apply(substitution.Domain[0]);

         error.CSharpLiteralExpression.ReplaceBy(factory.CreateExpression(genericParameter.IsStructType()
                                                                             ? "$0.FromResult(default($1))"
                                                                             : "$0.FromResult<$1>(null)",
                                                                          taskType,
                                                                          genericParameter));
      }

      return null;
   }

   #endregion
}