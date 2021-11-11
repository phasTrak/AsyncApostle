using AsyncApostle.AsyncHelpers.ConfigureAwaitCheckers;
using AsyncApostle.Highlightings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IAwaitExpression),
                        HighlightingTypes = new[]
                                            {
                                                typeof(ConfigureAwaitHighlighting)
                                            })]
public class ConfigureAwaitAnalyzer : ElementProblemAnalyzer<IAwaitExpression>
{
    #region methods

    protected override void Run(IAwaitExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
    {
        if (!element.GetSolution()
                    .GetComponent<IConfigureAwaitChecker>()
                    .NeedAdding(element))
            return;

        consumer.AddHighlighting(new ConfigureAwaitHighlighting(element));
    }

    #endregion
}