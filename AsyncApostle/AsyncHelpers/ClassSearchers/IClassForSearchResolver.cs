namespace AsyncApostle.AsyncHelpers.ClassSearchers;

public interface IClassForSearchResolver
{
   [Pure] ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType);
}