using System;
using System.Linq.Expressions;
using AsyncApostle.Settings.ConfigureAwaitOptions;
using AsyncApostle.Settings.General;
using JetBrains.Application.Settings;

namespace AsyncApostle.Settings;

public static class AsyncApostleSettingsAccessor
{
    #region fields

    public static readonly Expression<Func<AsyncApostleConfigureAwaitSettings, IIndexedEntry<string, string>?>> ConfigureAwaitIgnoreAttributeTypes = x => x.ConfigureAwaitIgnoreAttributeTypes;
    public static readonly Expression<Func<AsyncApostleConfigureAwaitSettings, bool>> ExcludeTestMethodsFromConfigureAwait = x => x.ExcludeTestMethodsFromConfigureAwait;
    public static readonly Expression<Func<GeneralSettings, bool>> ExcludeTestMethodsFromEliding = x => x.ExcludeTestMethodsFromEliding;
    public static readonly Expression<Func<GeneralSettings, bool>> ExcludeTestMethodsFromRenaming = x => x.ExcludeTestMethodsFromRenaming;

    #endregion
}