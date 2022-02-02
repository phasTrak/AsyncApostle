using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Search;
using static JetBrains.Application.Progress.NullProgressIndicator;
using static JetBrains.ReSharper.Psi.Search.FindExecution;

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
      List<TOverridableMember> found = new ();

      overridableMember.GetPsiServices()
                       .Finder.FindImplementingMembers(overridableMember,
                                                       overridableMember.GetSearchDomain(),
                                                       new FindResultConsumer(findResult =>
                                                                              {
                                                                                 if ((findResult as FindResultOverridableMember)?.OverridableMember is TOverridableMember result)
                                                                                    found.Add(result);

                                                                                 return Continue;
                                                                              }),
                                                       true,
                                                       pi ?? Create());

      return found;
   }

   static IEnumerable<IMethod> InnerFindAllHierarchy(IFinder finder, IMethod method, IProgressIndicator pi)
   {
      var immediateBaseMethods = finder.FindImmediateBaseElements(method, pi)
                                       .OfType<IMethod>()
                                       .ToArray();

      return immediateBaseMethods.Any()
                ? immediateBaseMethods.SelectMany(innerMethod => innerMethod.FindAllHierarchy())
                : new[] { method }.Concat(method.FindImplementingMembers());
   }

   static IEnumerable<IMethod> InnerFindBaseMethods(IFinder finder, IMethod method, IProgressIndicator pi) =>
      finder.FindImmediateBaseElements(method, pi)
            .OfType<IMethod>()
            .SelectMany(innerMethod => innerMethod.FindBaseMethods())
            .Concat(new[] { method });

   #endregion
}