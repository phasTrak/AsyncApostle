using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public class CompareResult
{
   #region properties

   public ParameterCompareResultAction Action { get; set; }
   public IType                        From   { get; set; } = default!;
   public IType                        To     { get; set; } = default!;

   #endregion
}