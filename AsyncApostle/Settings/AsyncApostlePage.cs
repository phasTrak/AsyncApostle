namespace AsyncApostle.Settings;

[OptionsPage(Pid,
             "Async Apostle",
             typeof(ServicesThemedIcons.FileStorage),
             ParentId = CodeInspectionPage.PID)]
public sealed class AsyncApostlePage : AEmptyOptionsPage
{
   #region fields

   public const string Pid = "AsyncApostle";

   #endregion
}