using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.ReSharper.Feature.Services.Resources;

namespace AsyncApostle.Settings;

[OptionsPage(PID,
             "Async Apostle",
             typeof(ServicesThemedIcons.FileStorage),
             ParentId = CodeInspectionPage.PID)]
public sealed class AsyncApostlePage : AEmptyOptionsPage
{
   #region fields

   public const string PID = "AsyncApostle";

   #endregion
}