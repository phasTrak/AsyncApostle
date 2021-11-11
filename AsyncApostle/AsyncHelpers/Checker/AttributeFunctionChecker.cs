using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using static AsyncApostle.Settings.AsyncApostleSettingsAccessor;

namespace AsyncApostle.AsyncHelpers.Checker;

[SolutionComponent]
public class AttributeFunctionChecker : IAttributeFunctionChecker
{
    #region methods

    public bool IsUnder(ICSharpTreeNode node)
    {
        var customTypes = node.GetSettingsStore()
                              .EnumIndexedValues(ConfigureAwaitIgnoreAttributeTypes)
                              .ToArray();

        return !customTypes.IsNullOrEmpty()
               && node.GetContainingFunctionDeclarationIgnoringClosures()
                      ?.ContainsAttribute(customTypes) is true;
    }

    #endregion
}