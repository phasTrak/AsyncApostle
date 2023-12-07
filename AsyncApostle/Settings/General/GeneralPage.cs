namespace AsyncApostle.Settings.General;

[OptionsPage(PID,
             "General",
             typeof(ServicesThemedIcons.AnalyzeThis),
             ParentId = AsyncApostlePage.PID)]
public sealed class GeneralPage : BeSimpleOptionsPage
{
   #region fields

   public const string PID = "General";

   #endregion

   #region constructors

   public GeneralPage(Lifetime lifetime, OptionsPageContext optionsPageContext, OptionsSettingsSmartContext store) : base(lifetime, optionsPageContext, store)
   {
      AddHeader("Naming options");
      AddBoolOption(static (GeneralSettings options) => options.ExcludeTestMethodsFromRenaming, "Do not suggest add 'Async' suffix to test method.");

      AddHeader("Eliding options");
      AddBoolOption(static (GeneralSettings options) => options.ExcludeTestMethodsFromEliding, "Do not suggest elide await in test method.");
   }

   #endregion
}