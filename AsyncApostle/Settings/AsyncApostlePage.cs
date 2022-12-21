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