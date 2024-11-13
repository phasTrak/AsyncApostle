namespace AsyncApostle.QuickFixes;

[QuickFix]
public class ArgumentValueAsTask(IncorrectArgumentTypeError error) : QuickFixBase
{
   #region properties

   public override string Text => "Replace to Task.FromResult";

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache)
   {
      var parameterType = error.ParameterType;

      if (!parameterType.IsGenericTask()) return false;

      var scalarType = parameterType.GetScalarType();

      if (scalarType is null) return false;

      var substitution = scalarType.GetSubstitution();

      return !substitution.IsEmpty()
          && error.ArgumentType?.ToIType()
                 ?.IsSubtypeOf(substitution.Apply(substitution.Domain[0])) is true;
   }

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      var expression = error.Argument as ICSharpArgument;

      if (expression?.GetContainingFile() is not ICSharpFile file) return null;

      var factory = GetInstance(expression);

      expression.ReplaceBy(factory.CreateArgument(VALUE, factory.CreateExpression("Task.FromResult($0)", expression)));

      if (file.ImportsEnumerable.OfType<IUsingSymbolDirective>()
              .All(static i => i.ImportedSymbolName.QualifiedName is not "System.Threading.Tasks"))
         file.AddImport(factory.CreateUsingDirective("System.Threading.Tasks"), true);

      return null;
   }

   #endregion
}