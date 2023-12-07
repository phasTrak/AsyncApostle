namespace AsyncApostle.AsyncHelpers.ParameterComparers;

[SolutionComponent]
class ParameterComparer(ITypeComparer typeComparer) : IParameterComparer
{
   #region methods

   public ParameterCompareResult ComparerParameters(IList<IParameter> originalParameters, IList<IParameter> methodParameters)
   {
      if (methodParameters.Count != originalParameters.Count) return ParameterCompareResult.CreateFailDifferentLength();

      var parameterResults = new CompareResult[methodParameters.Count];

      for (var i = 0; i < methodParameters.Count; i++)
      {
         var parameter         = methodParameters[i];
         var originalParameter = originalParameters[i];

         parameterResults[i] = new ()
                               {
                                  From   = originalParameter.Type,
                                  To     = parameter.Type,
                                  Action = typeComparer.Compare(originalParameter.Type, parameter.Type)
                               };
      }

      return ParameterCompareResult.Create(parameterResults);
   }

   #endregion
}