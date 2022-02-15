﻿using System.Collections.Generic;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.Util;
using Enumerable = System.Linq.Enumerable;

namespace AsyncApostle.AsyncHelpers.RenameCheckers;

[SolutionComponent]
class ControllerRenameChecker : IConcreteRenameChecker
{
   #region fields

   readonly HashSet<ClrTypeName> _controllerClasses = new ()
                                                      {
                                                         new ("System.Web.Mvc.Controller"),
                                                         new ("System.Web.Http.ApiController"),
                                                         new ("Microsoft.AspNetCore.Mvc.Controller"),
                                                         new ("Microsoft.AspNetCore.Mvc.ControllerBase")
                                                      };

   #endregion

   #region methods

   public bool SkipRename(IMethodDeclaration methodDeclaration) =>
      methodDeclaration.DeclaredElement?.GetContainingType() is IClass @class && Enumerable.Any(@class.GetSuperTypesWithoutCircularDependent(), superType => _controllerClasses.Contains(superType.GetClrName()));

   #endregion
}