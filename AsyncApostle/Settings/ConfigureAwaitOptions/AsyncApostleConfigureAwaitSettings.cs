namespace AsyncApostle.Settings.ConfigureAwaitOptions;

[SettingsKey(typeof(AsyncApostleSettings), "Settings for ConfigureAwait")]
public class AsyncApostleConfigureAwaitSettings
{
   [SettingsIndexedEntry("Custom attributes for ignoring ConfigureAwait.")]
   public required IIndexedEntry<string, string> ConfigureAwaitIgnoreAttributeTypes { get; set; }

   [SettingsEntry(true, "Do not suggest add 'ConfigureAwait' into test method.")]
   public bool ExcludeTestMethodsFromConfigureAwait { get; set; }
}