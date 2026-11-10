namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent(DemandAnyThreadSafe)]
public class ClassForSearchResolver(IEnumerable<IClassSearcher> classSearchers) : IClassForSearchResolver
{
   readonly IClassSearcher[] _classSearchers = [.. classSearchers.OrderBy(static x => x.Priority)];

   public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType) =>
      _classSearchers.Select(strategyResolver => strategyResolver.GetClassForSearch(originalMethod, invokedType))
                     .FirstOrDefault(static element => element is not null);
}