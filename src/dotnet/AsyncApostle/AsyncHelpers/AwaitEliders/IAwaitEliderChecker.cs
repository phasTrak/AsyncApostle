﻿using JetBrains.ReSharper.Psi.Tree;

namespace AsyncApostle.AsyncHelpers.AwaitEliders;

public interface IAwaitEliderChecker
{
   #region methods

   bool CanElide(IParametersOwnerDeclaration element);

   #endregion
}