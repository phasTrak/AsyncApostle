using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Resources;

namespace AsyncApostle.Settings.General;

[OptionsPage(PID, "General", typeof(ServicesThemedIcons.AnalyzeThis), ParentId = AsyncApostlePage.PID)]
public sealed class GeneralPage : BeSimpleOptionsPage
{
    #region fields

    public const string PID = "General";

    #endregion

    #region constructors

    public GeneralPage(Lifetime lifetime, OptionsPageContext optionsPageContext, OptionsSettingsSmartContext store) : base(lifetime, optionsPageContext, store)
    {
        AddHeader("Naming options");
        AddBoolOption((GeneralSettings options) => options.ExcludeTestMethodsFromRenaming, "Do not suggest add 'Async' suffix to test method.");

        AddHeader("Eliding options");
        AddBoolOption((GeneralSettings options) => options.ExcludeTestMethodsFromEliding, "Do not suggest elide await in test method.");
    }

    #endregion
}