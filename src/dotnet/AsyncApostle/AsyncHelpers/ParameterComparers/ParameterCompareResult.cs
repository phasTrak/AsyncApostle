using System;
using System.Linq;
using static System.Array;
using static AsyncApostle.AsyncHelpers.ParameterComparers.ParameterCompareAggregateResult;
using static AsyncApostle.AsyncHelpers.ParameterComparers.ParameterCompareResultAction;

namespace AsyncApostle.AsyncHelpers.ParameterComparers;

public class ParameterCompareResult
{
   #region constructors

   ParameterCompareResult(CompareResult[] compareResults)
   {
      ParameterResults = compareResults;
      Result = Resolve();
   }

   ParameterCompareResult() => ParameterResults = Empty<CompareResult>();

   #endregion

   #region properties

   public CompareResult[] ParameterResults { get; }
   public ParameterCompareAggregateResult Result { get; private init; }

   #endregion

   #region methods

   public static ParameterCompareResult Create(CompareResult[] compareResults) => new (compareResults);
   public static ParameterCompareResult CreateFailDifferentLength() => new () { Result = DifferentLength };
   static ParameterCompareAggregateResult Aggregate(ParameterCompareAggregateResult result, CompareResult compareResult) => Aggregate(result, Convert(compareResult));

   static ParameterCompareAggregateResult Aggregate(ParameterCompareAggregateResult result, ParameterCompareAggregateResult newResult) =>
      ToInt(result) < ToInt(newResult)
         ? result
         : newResult;

   static ParameterCompareAggregateResult Convert(CompareResult compareResult) =>
      compareResult.Action switch
      {
         ParameterCompareResultAction.Equal    => ParameterCompareAggregateResult.Equal,
         NeedConvertToAsyncFunc                => EqualOrCanBeConverting,
         ParameterCompareResultAction.NotEqual => ParameterCompareAggregateResult.NotEqual,
         _                                     => throw new ArgumentOutOfRangeException(nameof(compareResult.Action), compareResult.Action, null)
      };

   static int ToInt(ParameterCompareAggregateResult result) =>
      result switch
      {
         ParameterCompareAggregateResult.NotEqual => 10,
         DifferentLength                          => 0,
         ParameterCompareAggregateResult.Equal    => 30,
         EqualOrCanBeConverting                   => 50,
         _                                        => throw new ArgumentOutOfRangeException(nameof(result), result, null)
      };

   public bool CanBeConvertedToAsync() => Result is EqualOrCanBeConverting or ParameterCompareAggregateResult.Equal;
   ParameterCompareAggregateResult Resolve() => ParameterResults.Aggregate(ParameterCompareAggregateResult.Equal, Aggregate);

   #endregion
}