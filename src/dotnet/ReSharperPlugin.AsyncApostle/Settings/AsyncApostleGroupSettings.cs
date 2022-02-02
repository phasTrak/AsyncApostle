using JetBrains.ReSharper.Feature.Services.Daemon;

namespace AsyncApostle.Settings;

[RegisterConfigurableHighlightingsGroup(Id, Name)]
public static class AsyncApostleGroupSettings
{
   #region fields

   public const string Id = "AsyncApostle";
   public const string Name = "Async apostle plugin";

   #endregion
}