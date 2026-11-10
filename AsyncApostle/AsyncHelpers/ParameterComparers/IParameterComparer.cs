namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public interface IParameterComparer
{
   [Pure] ParameterCompareResult ComparerParameters(IList<IParameter> originalParameters, IList<IParameter> methodParameters);
}