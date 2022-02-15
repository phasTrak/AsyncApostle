using JetBrains.Application.Settings;

namespace AsyncApostle.Settings.General;

[SettingsKey(typeof(AsyncApostleSettings), "Settings for AsyncApostle plugin.")]
public class GeneralSettings
{
   #region properties

   [SettingsEntry(true, "Do not suggest elide await in test method.")]
   public bool ExcludeTestMethodsFromEliding { get; set; }

   [SettingsEntry(true, "Do not suggest add 'Async' suffix to test method.")]
   public bool ExcludeTestMethodsFromRenaming { get; set; }

   #endregion
}