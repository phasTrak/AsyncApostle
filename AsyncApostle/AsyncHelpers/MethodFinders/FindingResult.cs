namespace AsyncApostle.AsyncHelpers.MethodFinders;

public class FindingResult
{
   public IMethod?               Method                 { get; set; }
   public ParameterCompareResult ParameterCompareResult { get; set; } = null!;
   public static FindingResult CreateFail() => new ();
   public bool CanBeConvertedToAsync() => Method is not null && ParameterCompareResult.CanBeConvertedToAsync();
}