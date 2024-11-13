namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent(DemandAnyThreadSafe)]
public class DefaultSearcher : IClassSearcher
{
   #region properties

   public int Priority => 0;

   #endregion

   #region methods

   public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType) => originalMethod.GetContainingType();

   #endregion
}