namespace System.Runtime.CompilerServices
{
   static class IsExternalInit;

   class RequiredMemberAttribute : Attribute;

   class CompilerFeatureRequiredAttribute(string name) : Attribute
   {
      #region properties

      internal string Name { get; } = name;

      #endregion
   }
}

namespace System.Diagnostics.CodeAnalysis
{
   [AttributeUsage(AttributeTargets.Constructor)] class SetsRequiredMembersAttribute : Attribute;
}