namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent]
public class ClassForSearchResolver : IClassForSearchResolver
{
   #region fields

   readonly IClassSearcher[] _classSearchers;

   #endregion

   #region constructors

   public ClassForSearchResolver(IEnumerable<IClassSearcher> classSearchers) =>
      _classSearchers = classSearchers.OrderBy(x => x.Priority)
                                      .ToArray();

   #endregion

   #region methods

   public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType) =>
      _classSearchers.Select(strategyResolver => strategyResolver.GetClassForSearch(originalMethod, invokedType))
                     .FirstOrDefault(element => element is not null);

   #endregion
}