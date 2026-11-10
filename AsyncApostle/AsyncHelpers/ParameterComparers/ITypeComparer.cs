namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public interface ITypeComparer
{
   ParameterCompareResultAction Compare(IType originalParameterType, IType parameterType);
}