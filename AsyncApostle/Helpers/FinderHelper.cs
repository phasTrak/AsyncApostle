namespace AsyncApostle.Helpers;

public static class FinderHelper
{
   #region methods

   public static IEnumerable<IMethod> FindAllHierarchy(this IMethod method, IProgressIndicator? pi = default) =>
      InnerFindAllHierarchy(method.GetPsiServices()
                                  .Finder,
                            method,
                            pi ?? Create());

   public static IEnumerable<IMethod> FindBaseMethods(this IMethod method, IProgressIndicator? pi = null) =>
      InnerFindBaseMethods(method.GetPsiServices()
                                 .Finder,
                           method,
                           pi ?? Create());

   public static IList<TOverridableMember> FindImplementingMembers<TOverridableMember>(this TOverridableMember overridableMember, IProgressIndicator? pi = null) where TOverridableMember : class, IOverridableMember
   {
      List<TOverridableMember> found = [];

      overridableMember.GetPsiServices()
                       .Finder.FindImplementingMembers(overridableMember,
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
         ..finder.FindImmediateBaseElements(method, pi)
                 .OfType<IMethod>()
      ];

      return immediateBaseMethods.Any()
                ? immediateBaseMethods.SelectMany(static innerMethod => innerMethod.FindAllHierarchy())
                : [method, ..method.FindImplementingMembers()];
   }

   static IEnumerable<IMethod> InnerFindBaseMethods(IFinder finder, IMethod method, IProgressIndicator pi) =>
   [
      ..finder.FindImmediateBaseElements(method, pi)
              .OfType<IMethod>()
              .SelectMany(static innerMethod => innerMethod.FindBaseMethods()),
      method
   ];

   #endregion
}