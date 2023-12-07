namespace AsyncApostle.Helpers;

public static class EnumerableExtension
{
   #region methods

   public static HashSet<TItem> ToHashSet<TItem>(this IEnumerable<TItem> items) => [..items];

   #endregion
}