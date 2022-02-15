using AsyncApostle.Highlightings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using static JetBrains.ReSharper.Psi.CSharp.Parsing.CSharpTokenType;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IReturnStatement), HighlightingTypes = new[] { typeof(NullReturnAsTaskHighlighting) })]
public class NullReturnFromMethodAnalyzer : ElementProblemAnalyzer<IReturnStatement>
{
   #region methods

   protected override void Run(IReturnStatement element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      var literalExpression = element.Value as ICSharpLiteralExpression;

      if (literalExpression?.Literal.GetTokenType() != NULL_KEYWORD)
         return;

      switch (element.GetContainingFunctionLikeDeclarationOrClosure())
      {
         case IAnonymousFunctionExpression lambda when !lambda.InferredReturnType.IsTask() && !lambda.InferredReturnType.IsGenericTask():
            return;
         case IAnonymousFunctionExpression { IsAsync: true }:
            return;
         case IAnonymousFunctionExpression lambda:
            consumer.AddHighlighting(new NullReturnAsTaskHighlighting(literalExpression, lambda.InferredReturnType));

            break;
         case IMethodDeclaration method when !method.Type.IsTask() && !method.Type.IsGenericTask():
            return;
         case IMethodDeclaration { IsAsync: true }:
            return;
         case IMethodDeclaration method:
            consumer.AddHighlighting(new NullReturnAsTaskHighlighting(literalExpression, method.Type));

            break;
      }
   }

   #endregion
}