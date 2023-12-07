namespace AsyncApostle.QuickFixes;

[QuickFix]
public class AsyncMethodNamingQuickFix(AsyncMethodNamingHighlighting asyncMethodNamingHighlighting) : QuickFixBase
{
   #region properties

   public override string Text => """Add "Async" suffix""";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache) => asyncMethodNamingHighlighting.IsValid();

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      var methodDeclaration = asyncMethodNamingHighlighting.MethodDeclaration.DeclaredElement;

      if (methodDeclaration is null) return null;

      Rename(solution, new (methodDeclaration, $"{methodDeclaration.ShortName}Async"));

      return null;
   }

   #endregion
}