namespace AsyncApostle.Analyzers;

[ElementProblemAnalyzer(typeof(IMethodDeclaration), HighlightingTypes = [typeof(AsyncMethodNamingHighlighting)])]
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