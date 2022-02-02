using AsyncApostle.AsyncHelpers.MissingAwaitChecker;
using AsyncApostle.Helpers;
using AsyncApostle.Highlightings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IParametersOwnerDeclaration),
    HighlightingTypes = new[] { typeof(MissingAwaitHighlighting) })]
public class MissingAwaitAnalyzer : ElementProblemAnalyzer<IParametersOwnerDeclaration>
{
    #region methods

    protected override void Run(IParametersOwnerDeclaration element, ElementProblemAnalyzerData data,
        IHighlightingConsumer consumer)
    {
        if (!element.GetSolution()
                .GetComponent<IMissingAwaitChecker>()
                .AwaitIsMissing(element))
            return;

        foreach (var awaitExpression in element.DescendantsInScope<IInvocationExpression>())
            consumer.AddHighlighting(new MissingAwaitHighlighting(awaitExpression));
    }

    #endregion
}