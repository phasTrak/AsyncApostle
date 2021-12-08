using AsyncApostle.Helpers;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using static JetBrains.Metadata.Reader.API.NullableAnnotation;
using static JetBrains.ReSharper.Psi.TypeFactory;

namespace AsyncApostle.AsyncHelpers.ClassSearchers;

[SolutionComponent]
public class EntityFrameworkCustomSearcher : IClassSearcher
{
   #region properties

   public int Priority => -100;

   #endregion

   #region methods

   public ITypeElement? GetClassForSearch(IParametersOwner originalMethod, IType? invokedType)
   {
      var containingType = originalMethod.GetContainingType();

      return containingType is null                                                                ? null :
             invokedType?.IsGenericIQueryable() is not true || !containingType.IsEnumerableClass() ? null : CreateTypeByCLRName(new ClrTypeName("System.Data.Entity.QueryableExtensions"), Unknown, invokedType.Module)
                                                                                                       .GetTypeElement();
   }

   #endregion
}