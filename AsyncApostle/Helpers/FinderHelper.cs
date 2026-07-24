namespace AsyncApostle.Helpers;

public static class FinderHelper
{
   #region methods

   extension(IMethod method)
   {
      public IEnumerable<IMethod> FindAllHierarchy(IProgressIndicator? pi = null) =>
         InnerFindAllHierarchy(method.GetPsiServices()
                                     .SingleThreadedFinder,
                               method,
                               pi ?? Create());

      public IEnumerable<IMethod> FindBaseMethods(IProgressIndicator? pi = null) =>
         InnerFindBaseMethods(method.GetPsiServices()
                                    .SingleThreadedFinder,
                              method,
                              pi ?? Create());
   }

   public static IList<TOverridableMember> FindImplementingMembers<TOverridableMember>(this TOverridableMember overridableMember, IProgressIndicator? pi = null) where TOverridableMember : class, IOverridableMember
   {
      List<TOverridableMember> found = [];

      overridableMember.GetPsiServices()
                       .SingleThreadedFinder.FindImplementingMembers(overridableMember,
                                                                     overridableMember.GetSearchDomain(),
                                                                     new FindResultConsumer(findResult =>
                                                                                            {
                                                                                               if (findResult is FindResultOverridableMember { OverridableMember: TOverridableMember result }) found.Add(result);

                                                                                               return Continue;
                                                                                            }),
                                                                     true,
                                                                     pi ?? Create());

      return found;
   }

   static IEnumerable<IMethod> InnerFindAllHierarchy(IFinder finder, IMethod method, IProgressIndicator pi)
   {
      IMethod[] immediateBaseMethods =
      [
         .. finder.FindImmediateBaseElements(method, pi)
                  .OfType<IMethod>()
      ];

      return immediateBaseMethods.Any()
                ? immediateBaseMethods.SelectMany(static innerMethod => innerMethod.FindAllHierarchy())
                : [method, .. method.FindImplementingMembers()];
   }

   static IEnumerable<IMethod> InnerFindBaseMethods(IFinder finder, IMethod method, IProgressIndicator pi) =>
   [
      .. finder.FindImmediateBaseElements(method, pi)
               .OfType<IMethod>()
               .SelectMany(static innerMethod => innerMethod.FindBaseMethods()),
      method
   ];

   #endregion
}