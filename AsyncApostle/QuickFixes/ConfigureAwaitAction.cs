namespace AsyncApostle.QuickFixes;

public class ConfigureAwaitAction(ConfigureAwaitHighlighting configureAwaitHighlighting, bool configureAwaitValue) : BulbActionBase
{
   public override string Text => $"Add ConfigureAwait({GetConfigureAwaitValueText()})";

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      var awaitExpression = configureAwaitHighlighting.AwaitExpression;

      awaitExpression.Task.ReplaceBy(GetInstance(awaitExpression)
                                       .CreateExpression($"$0.ConfigureAwait({GetConfigureAwaitValueText()})", awaitExpression.Task));

      return null;
   }

   string GetConfigureAwaitValueText() =>
      configureAwaitValue
         ? "true"
         : "false";
}