namespace AsyncApostle.AsyncHelpers.ClassSearchers;

public interface IClassSearcher
{
   int Priority { get; }
   [Pure] ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType);
}