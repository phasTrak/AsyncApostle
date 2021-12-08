using AsyncApostle.AsyncHelpers.ParameterComparers;
using JetBrains.ReSharper.Psi;

namespace AsyncApostle.AsyncHelpers.MethodFinders;

public class FindingResult
{
   #region properties

   public IMethod? Method { get; set; }
   public ParameterCompareResult ParameterCompareResult { get; set; } = default!;

   #endregion

   #region methods

   public static FindingResult CreateFail() => new ();
   public bool CanBeConvertedToAsync() => Method is not null && ParameterCompareResult.CanBeConvertedToAsync();

   #endregion
}