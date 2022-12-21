namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public interface IParameterComparer
{
   #region methods

   [Pure] ParameterCompareResult ComparerParameters(IList<IParameter> originalParameters, IList<IParameter> methodParameters);

   #endregion
}