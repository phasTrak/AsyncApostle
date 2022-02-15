﻿using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace AsyncApostle.AsyncHelpers;

public static class InvocationExpressionExtensions
{
   #region methods

   [Pure]
   [ContractAnnotation("null => null")]
   public static ICSharpExpression RemoveConfigureAwait(this IInvocationExpression expression)
   {
      //var expressionWithoutConfigureAwait = (expression.FirstChild as IReferenceExpression)?.QualifierExpression;
      //if (expressionWithoutConfigureAwait == null)
      //    return expression;

      //return expressionWithoutConfigureAwait;

      ICSharpExpression? expressionWithoutConfigureAwait;

      // TODO: don't understand
      //var xmlDocId = (expression?.Reference?.Resolve().DeclaredElement as IMethod)?.XMLDocId;
      //if (xmlDocId == "M:System.Threading.Tasks.Task.ConfigureAwait(System.Boolean)"
      //    || xmlDocId == "M:System.Threading.Tasks.Task`1.ConfigureAwait(System.Boolean)")

      // TODO: don't understand, tmp hack
      if (expression.Reference.GetName() is "ConfigureAwait")
      {
         expressionWithoutConfigureAwait = (expression.FirstChild as IReferenceExpression)?.QualifierExpression;

         if (expressionWithoutConfigureAwait is null)
            return expression;
      }
      else
         expressionWithoutConfigureAwait = expression;

      return expressionWithoutConfigureAwait;
   }

   #endregion
}