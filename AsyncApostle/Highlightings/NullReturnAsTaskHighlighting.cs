namespace AsyncApostle.Highlightings;

[RegisterConfigurableSeverity(SeverityId,
                              null,
                              Id,
                              "Null return from async method",
                              "May cause null reference exception if return of method will be awaiting.",
                              WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public class NullReturnAsTaskHighlighting(ICSharpLiteralExpression cSharpLiteralExpression, IType returnType) : IHighlighting
{
   #region fields

   public const string SeverityId = "AsyncApostle.NullReturnAsTask";

   #endregion

   #region properties

   public ICSharpLiteralExpression CSharpLiteralExpression { get; } = cSharpLiteralExpression;
   public string                   ErrorStripeToolTip      => "May cause null reference if Task will be await.";
   public IType                    ReturnType              { get; } = returnType;
   public string                   ToolTip                 => "Null return as Task";

   #endregion

   #region methods

   public DocumentRange CalculateRange() => CSharpLiteralExpression.GetDocumentRange();
   public bool IsValid() => CSharpLiteralExpression.IsValid() && ReturnType.IsValid();

   #endregion
}