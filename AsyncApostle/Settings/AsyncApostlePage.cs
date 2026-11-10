namespace AsyncApostle.Settings;

[OptionsPage(Pid,
             "Async Apostle",
             typeof(ServicesThemedIcons.FileStorage),
             ParentId = CodeInspectionPage.PID)]
public sealed class AsyncApostlePage : AEmptyOptionsPage
{
   public const string Pid = "AsyncApostle";
}