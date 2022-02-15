using System;
using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;

namespace AsyncApostle.AsyncHelpers.MissingAwaitChecker;

[SolutionComponent]
public class MissingAwaitChecker : IMissingAwaitChecker
{
   /// <inheritdoc />
   public bool AwaitIsMissing(IParametersOwnerDeclaration element)
   {
      var returnStatement = element.DescendantsInScope<IReturnStatement>().FirstOrDefault();
      var invocationExpressions = element.DescendantsInScope<IInvocationExpression>()
         .ToArray();

      foreach (var invocationExpression in invocationExpressions)
      {
         if (!InvocationCouldBeAwaited(invocationExpression)) continue;

         var invocationIsReturned = invocationExpression.GetContainingStatement() == returnStatement;
         if (!InvocationIsAwaited(invocationExpression) && !invocationIsReturned) return true;
      }

      return false;
   }

   private static bool InvocationCouldBeAwaited(IExpression invocationExpression)
   {
      var invocationType = invocationExpression.Type().GetTypeElement();
      var getAwaiterMethod = invocationType?.Methods.FirstOrDefault(method => ShortNamesMatch(method, "GetAwaiter"));
      if (getAwaiterMethod == null) return false;

      var getAwaiterType = getAwaiterMethod.Type().GetTypeElement();
      var isCompletedProperty = getAwaiterType?.Properties.FirstOrDefault(prop => ShortNamesMatch(prop, "IsCompleted"));
      if (isCompletedProperty == null) return false;

      var onCompletedMethod = getAwaiterType?.Methods.FirstOrDefault(method => ShortNamesMatch(method, "OnCompleted"));
      if (onCompletedMethod == null) return false;

      var getResultMethod = getAwaiterType?.Methods.FirstOrDefault(method => ShortNamesMatch(method, "GetResult"));
      if (getResultMethod == null) return false;

      return true;
   }

   private bool InvocationIsAwaited(ITreeNode treeNode)
   {
      if (treeNode is IAwaitExpression || treeNode.Parent is IAwaitExpression)
         return true;

      return treeNode.Parent != null && InvocationIsAwaited(treeNode.Parent);
   }

   private static bool ShortNamesMatch(IDeclaredElement element, string shortName) =>
      element.ShortName.Equals(shortName, StringComparison.Ordinal);
}