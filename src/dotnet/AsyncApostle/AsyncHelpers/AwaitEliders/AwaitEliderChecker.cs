﻿using System.Collections.Generic;
using System.Linq;
using AsyncApostle.Helpers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.AwaitEliders;

[SolutionComponent]
public class AwaitEliderChecker : IAwaitEliderChecker
{
   #region fields

   readonly IConcreteAwaitEliderChecker[] _checkers;

   #endregion

   #region constructors

   public AwaitEliderChecker(IEnumerable<IConcreteAwaitEliderChecker> checkers) => _checkers = checkers.ToArray();

   #endregion

   #region methods

   public bool CanElide(IParametersOwnerDeclaration element) => _checkers.All(x => x.CanElide(element));

   #endregion
}