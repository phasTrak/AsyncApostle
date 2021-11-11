using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using static AsyncApostle.Settings.AsyncApostleSettingsAccessor;

namespace AsyncApostle.Checkers;

[SolutionComponent]
public class AttributeTypeChecker : IAttributeTypeChecker
{
    #region methods

    public bool IsUnder(ICSharpTreeNode node)
    {
        var customTypes = node.GetSettingsStore()
                              .EnumIndexedValues(ConfigureAwaitIgnoreAttributeTypes)
                              .ToArray();

        return !customTypes.IsNullOrEmpty()
               && node.GetContainingTypeDeclaration()
                      ?.ContainsAttribute(customTypes) is true;
    }

    #endregion
}