namespace AsyncApostle.Helpers;

public static class EnumerableExtension
{
   public static HashSet<TItem> ToHashSet<TItem>(this IEnumerable<TItem> items) => [.. items];
}