using AsyncApostle.Highlightings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using static JetBrains.ReSharper.Psi.CSharp.Parsing.CSharpTokenType;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(ILambdaExpression),
                        HighlightingTypes = new[]
                                            {
                                                typeof(NullReturnAsTaskHighlighting)
                                            })]
public class NullReturnFromLambdaAnalyzer : ElementProblemAnalyzer<ILambdaExpression>
{
    #region methods

    protected override void Run(ILambdaExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
    {
        var literalExpression = element.BodyExpression as ICSharpLiteralExpression;

        if (literalExpression?.Literal.GetTokenType() != NULL_KEYWORD)
            return;

        if (element.IsAsync)
            return;

        if (!element.InferredReturnType.IsTask() && !element.InferredReturnType.IsGenericTask())
            return;

        consumer.AddHighlighting(new NullReturnAsTaskHighlighting(literalExpression, element.InferredReturnType));
    }

    #endregion
}