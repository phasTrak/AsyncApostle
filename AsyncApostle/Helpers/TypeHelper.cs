using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using JetBrains.Diagnostics;
using JetBrains.Metadata.Reader.API;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using static System.Runtime.CompilerServices.MethodImplOptions;
using static System.StringComparison;
using static JetBrains.Metadata.Reader.API.NullableAnnotation;
using static JetBrains.ReSharper.Psi.Impl.DeclaredElementEqualityComparer;
using static JetBrains.ReSharper.Psi.PredefinedType;

namespace AsyncApostle.Helpers;

public static class TypeHelper
{
   #region methods

   [Pure]
   [ContractAnnotation("null => null")]
   public static IType? GetFirstGenericType(this IType type)
   {
      if (type is not IDeclaredType taskDeclaredType)
         return null;

      var substitution = taskDeclaredType.GetSubstitution();

      return substitution.IsEmpty()
                ? null
                : substitution.Apply(substitution.Domain[0]);
   }

   public static bool IsAsyncDelegate(this IType type, IType otherType)
   {
      if (type.IsAction() && otherType.IsFunc())
      {
         var substitution1 = (otherType as IDeclaredType)?.GetSubstitution();

         return substitution1?.Domain.Count is 1
             && substitution1.Apply(substitution1.Domain[0])
                             .IsTask();
      }

      if (!type.IsFunc() || !otherType.IsFunc())
         return false;

      var substitution = (otherType as IDeclaredType)?.GetSubstitution();
      var originalSubstitution = (type as IDeclaredType)?.GetSubstitution();

      if (substitution is null || substitution.Domain.Count != originalSubstitution?.Domain.Count)
         return false;

      var i = 0;

      for (; i < substitution.Domain.Count - 1; i++)
      {
         if (!substitution.Apply(substitution.Domain[i])
                          .Equals(originalSubstitution.Apply(originalSubstitution.Domain[i])))
            return false;
      }

      return substitution.Apply(substitution.Domain[i])
                         .IsGenericTaskOf(originalSubstitution.Apply(originalSubstitution.Domain[i]));
   }

   [Pure]
   [ContractAnnotation("null => false")]
   public static bool IsEnumerableClass(this ITypeElement? type) =>
      type?.GetClrName()
           .Equals(ENUMERABLE_CLASS) is true;

   public static bool IsEquals(this IType type, IType otherType)
   {
      if (!type.IsOpenType && !otherType.IsOpenType)
         return Equals(type, otherType);

      if (!IsEqualTypeGroup(type, otherType))
         return false;

      var scalarType = type.GetScalarType();
      var otherScalarType = otherType.GetScalarType();

      if (scalarType is null || otherScalarType is null || scalarType.Classify != otherScalarType.Classify)
         return false;

      var typeElement1 = scalarType.GetTypeElement();
      var typeElement2 = otherScalarType.GetTypeElement();

      return typeElement1 is not null
          && typeElement2 is not null
          && (typeElement1 is not ITypeParameter typeParameter1 || typeElement2 is not ITypeParameter typeParameter2
                 ? EqualSubstitutions(typeElement1, scalarType.GetSubstitution(), typeElement2, otherScalarType.GetSubstitution())
                 : typeParameter1.HasDefaultConstructor == typeParameter2.HasDefaultConstructor
                && typeParameter1.TypeConstraints.Count == typeParameter2.TypeConstraints.Count
                && !typeParameter1.TypeConstraints.Where((t, i) => !t.IsEquals(typeParameter2.TypeConstraints[i]))
                                  .Any()
                && EqualSubstitutions(typeElement1, scalarType.GetSubstitution(), typeElement2, otherScalarType.GetSubstitution()));
   }

   [Pure]
   [ContractAnnotation("null => false")]
   public static bool IsFunc(this IType? type)
   {
      if (type is not IDeclaredType declaredType)
         return false;

      var clrTypeName = declaredType.GetClrName();

      return clrTypeName.Equals(FUNC_FQN) || clrTypeName.FullName.StartsWith($"{FUNC_FQN}`", Ordinal);
   }

   [Pure]
   [ContractAnnotation("null => false")]
   public static bool IsGenericIQueryable(this IType? type) => type is IDeclaredType declaredType && IsPredefinedTypeElement(declaredType.GetTypeElement(), GENERIC_IQUERYABLE_FQN);

   [Pure]
   [ContractAnnotation("type:null => false; otherType:null => false")]
   public static bool IsGenericTaskOf(this IType? type, IType? otherType) =>
      type is not null
   && otherType is not null
   && type.IsGenericTask()
   && type.GetFirstGenericType()
         ?.IsEquals(otherType) is true;

   [Pure]
   [ContractAnnotation("type:null => false; otherType:null => false")]
   public static bool IsTaskOf(this IType? type, IType? otherType) => type.IsTask() && otherType.IsVoid() || type.IsGenericTaskOf(otherType);

   static bool EqualSubstitutions(ITypeElement referenceOwner,
                                  ISubstitution referenceSubstitution,
                                  ITypeElement originOwner,
                                  ISubstitution originSubstitution)
   {
      foreach (var substitution1 in referenceOwner.GetAncestorSubstitution(originOwner))
      {
         var substitution2 = substitution1.Apply(referenceSubstitution);

         if (substitution2.Domain.Any(typeParameter => originSubstitution.HasInDomain(typeParameter)
                                                    && !substitution2[typeParameter]
                                                         .IsEquals(originSubstitution[typeParameter])))
            return false;
      }

      return true;
   }

   static bool IsEqualTypeGroup(IType sourceType, IType targetType) =>
      sourceType.IsOpenType == targetType.IsOpenType
   && (sourceType is IDeclaredType && targetType is IDeclaredType || sourceType is IArrayType && targetType is IArrayType || sourceType is IPointerType && targetType is IPointerType);

   [Pure]
   [ContractAnnotation("typeElement:null => false")]
   [MethodImpl(AggressiveInlining)]
   static bool IsPredefinedTypeElement(ITypeElement? typeElement, IClrTypeName clrName) =>
      typeElement is not null
   && TypeElementComparer.Equals(typeElement,
                                 typeElement.Module.GetPredefinedType()
                                            .TryGetType(clrName, Unknown)
                                            .NotNull("NOT PREDEFINED")
                                            .GetTypeElement());

   #endregion
}