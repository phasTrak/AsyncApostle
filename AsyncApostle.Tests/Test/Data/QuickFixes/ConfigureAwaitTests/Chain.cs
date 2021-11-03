using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync()
        {
            var tmp = (await {caret}Task.FromResult(new[] { 1 })).Select(x => x + 1);
        }
    }
}
