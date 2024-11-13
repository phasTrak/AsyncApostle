namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent(DemandAnyThreadSafe)]
public class ClassForSearchResolver(IEnumerable<IClassSearcher> classSearchers) : IClassForSearchResolver
{
   #region fields

   readonly IClassSearcher[] _classSearchers = [..classSearchers.OrderBy(static x => x.Priority)];

   #endregion

   #region methods

   public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType) =>
      _classSearchers.Select(strategyResolver => strategyResolver.GetClassForSearch(originalMethod, invokedType))
                     .FirstOrDefault(static element => element is not null);

   #endregion
}