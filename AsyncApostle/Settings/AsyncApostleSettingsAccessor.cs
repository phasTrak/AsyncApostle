namespace AsyncApostle.Settings;

public static class AsyncApostleSettingsAccessor
{
   #region fields

   public static readonly Expression<Func<AsyncApostleConfigureAwaitSettings, IIndexedEntry<string, string>?>> ConfigureAwaitIgnoreAttributeTypes   = static x => x.ConfigureAwaitIgnoreAttributeTypes;
   public static readonly Expression<Func<AsyncApostleConfigureAwaitSettings, bool>>                           ExcludeTestMethodsFromConfigureAwait = static x => x.ExcludeTestMethodsFromConfigureAwait;
   public static readonly Expression<Func<GeneralSettings, bool>>                                              ExcludeTestMethodsFromEliding        = static x => x.ExcludeTestMethodsFromEliding;
   public static readonly Expression<Func<GeneralSettings, bool>>                                              ExcludeTestMethodsFromRenaming       = static x => x.ExcludeTestMethodsFromRenaming;

   #endregion
}