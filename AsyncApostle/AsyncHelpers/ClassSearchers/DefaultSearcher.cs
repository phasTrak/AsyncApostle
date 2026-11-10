namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent(DemandAnyThreadSafe)]
public class DefaultSearcher : IClassSearcher
{
   public int Priority => 0;
   public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType) => originalMethod.GetContainingType();
}