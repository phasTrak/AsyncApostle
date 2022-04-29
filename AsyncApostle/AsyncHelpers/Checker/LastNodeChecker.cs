using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.CSharp.Util;

namespace AsyncApostle.AsyncHelpers.Checker;

[SolutionComponent]
class LastNodeChecker : ILastNodeChecker
{
   #region methods

   static bool IsFinalStatement(ICSharpStatement statement)
   {
      var focus = statement;

      while (focus.GetNextStatement() is null)
      {
         if (focus is null) return true;

         focus = focus.GetContainingStatement();
      }

      return false;
   }

   public bool IsLastNode(ICSharpExpression element)
   {
      var parentStatement = element.Parent as ICSharpStatement;

      return parentStatement is IReturnStatement or IExpressionStatement
          && IsFinalStatement(parentStatement)
          && parentStatement.GetContainingNode<IUsingStatement>() is null
          && parentStatement.GetContainingNode<ITryStatement>() is null
          && parentStatement.GetContainingNode<ILoopStatement>() is null
          && parentStatement.GetContainingNode<IIfStatement>() is null
          || element.Parent is IArrowExpressionClause or ILambdaExpression;
   }

   #endregion
}