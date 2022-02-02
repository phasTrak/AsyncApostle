using AsyncApostle.AsyncHelpers.RenameCheckers;
using AsyncApostle.Highlightings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IMethodDeclaration), HighlightingTypes = new[] { typeof(AsyncMethodNamingHighlighting) })]
public class AsyncMethodNamingAnalyzer : ElementProblemAnalyzer<IMethodDeclaration>
{
   #region methods

   protected override void Run(IMethodDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
   {
      if (!element.GetSolution()
                  .GetComponent<IRenameChecker>()
                  .NeedRename(element))
         return;

      consumer.AddHighlighting(new AsyncMethodNamingHighlighting(element));
   }

   #endregion
}