namespace AsyncApostle.AsyncHelpers;

public static class InvocationExpressionExtensions
{
   #region methods

   [Pure]
   [ContractAnnotation("null => null")]
   public static ICSharpExpression RemoveConfigureAwait(this IInvocationExpression expression)
   {
      ICSharpExpression? expressionWithoutConfigureAwait;

      // TODO: don't understand, tmp hack
      if (expression.Reference.GetName() is "ConfigureAwait")
      {
         expressionWithoutConfigureAwait = (expression.FirstChild as IReferenceExpression)?.QualifierExpression;

         if (expressionWithoutConfigureAwait is null) return expression;
      }
      else
         expressionWithoutConfigureAwait = expression;

      return expressionWithoutConfigureAwait;
   }

   #endregion
}