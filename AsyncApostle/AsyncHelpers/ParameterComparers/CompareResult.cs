namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public class CompareResult
{
   public ParameterCompareResultAction Action { get; set; }
   public IType                        From   { get; set; } = null!;
   public IType                        To     { get; set; } = null!;
}