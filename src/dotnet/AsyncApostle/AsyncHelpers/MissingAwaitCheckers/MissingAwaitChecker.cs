﻿using System;
using System.Linq;
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
   public bool AwaitIsMissing(ICSharpTreeNode cSharpTreeNode)
   {
      var invocationExpression = cSharpTreeNode.GetContainingNode<IInvocationExpression>();
      if (invocationExpression == null) return false;

      if (!InvocationCouldBeAwaited(invocationExpression)) return false;

      var invocationIsReturned = cSharpTreeNode.GetContainingNode<IReturnStatement>() != null;
      if (!InvocationIsAwaited(invocationExpression) && !invocationIsReturned) return true;

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

      if (IsSynchronouslyAwaited(treeNode)) return true;

      return treeNode.Parent != null && InvocationIsAwaited(treeNode.Parent);
   }

   /// <summary>
   ///    Checks if <c>GetAwaiter().GetResult()</c> is used.
   /// </summary>
   private static bool IsSynchronouslyAwaited(ITreeNode treeNode)
   {
      if (treeNode is IReferenceExpression r1 && r1.NameIdentifier.Name.Equals("GetAwaiter", StringComparison.Ordinal))
         if (treeNode.Parent?.Parent is IReferenceExpression r2 &&
             r2.NameIdentifier.Name.Equals("GetResult", StringComparison.Ordinal))
            return true;

      return false;
   }

   private static bool ShortNamesMatch(IDeclaredElement element, string shortName) =>
      element.ShortName.Equals(shortName, StringComparison.Ordinal);
}