namespace AsyncApostle.Settings.ConfigureAwaitOptions;

[OptionsPage(Pid,
             "ConfigureAwait settings",
             typeof(ServicesThemedIcons.InspectionToolWindow),
             ParentId = AsyncApostlePage.Pid)]
public sealed class AsyncApostleConfigureAwaitPage : BeSimpleOptionsPage
{
   #region fields

   public const string Pid = "AsyncApostleConfigureAwait";

   #endregion

   #region constructors

   public AsyncApostleConfigureAwaitPage(Lifetime lifetime, OptionsPageContext optionsPageContext, OptionsSettingsSmartContext store) : base(lifetime, optionsPageContext, store) =>
      AddBoolOption(static (AsyncApostleConfigureAwaitSettings options) => options.ExcludeTestMethodsFromConfigureAwait, "Do not suggest add 'ConfigureAwait' in test method.");

   #endregion
}