using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Resources;

namespace AsyncApostle.Settings.ConfigureAwaitOptions;

[OptionsPage(PID, "ConfigureAwait settings", typeof(ServicesThemedIcons.InspectionToolWindow), ParentId = AsyncApostlePage.PID)]
public sealed class AsyncApostleConfigureAwaitPage : BeSimpleOptionsPage
{
   #region fields

   public const string PID = "AsyncApostleConfigureAwait";

   #endregion

   #region constructors

   public AsyncApostleConfigureAwaitPage(Lifetime lifetime, OptionsPageContext optionsPageContext, OptionsSettingsSmartContext store) : base(lifetime, optionsPageContext, store) =>
      AddBoolOption((AsyncApostleConfigureAwaitSettings options) => options.ExcludeTestMethodsFromConfigureAwait, "Do not suggest add 'ConfigureAwait' in test method.");

   #endregion
}