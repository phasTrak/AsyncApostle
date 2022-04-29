using System;
using AsyncApostle.Helpers;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace AsyncApostle.ContextActions;

[ContextAction(Group = "C#", Name = "ConvertToAsync", Description = "Convert method to async and replace all inner call to async version if exist.")]
public class MethodToAsyncApostle : ContextActionBase
{
   #region fields

   IAsyncReplacer? _asyncReplacer;

   #endregion

   #region constructors

   public MethodToAsyncApostle(ICSharpContextActionDataProvider provider) => Provider = provider;

   #endregion

   #region properties

   public override string           Text     => "Convert method to async and replace all inner call to async version if exist.";
   ICSharpContextActionDataProvider Provider { get; }

   #endregion

   #region methods

   public override bool IsAvailable(IUserDataHolder cache)
   {
      var method = GetMethodFromCaretPosition();

      if (method is null) return false;

      var returnType = method.DeclaredElement?.ReturnType;

      return !(returnType?.IsTask() is true || returnType?.IsGenericTask() is true);
   }

   protected override Action<ITextControl>? ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
   {
      _asyncReplacer = solution.GetComponent<IAsyncReplacer>();

      var methodDeclaredElement = GetMethodFromCaretPosition()
       ?.DeclaredElement;

      if (methodDeclaredElement is null) return null;

      _asyncReplacer.ReplaceToAsync(methodDeclaredElement);

      return null;
   }

   IMethodDeclaration? GetMethodFromCaretPosition() => (Provider.TokenAfterCaret as ICSharpIdentifier ?? Provider.TokenBeforeCaret as ICSharpIdentifier)?.Parent as IMethodDeclaration;

   #endregion
}