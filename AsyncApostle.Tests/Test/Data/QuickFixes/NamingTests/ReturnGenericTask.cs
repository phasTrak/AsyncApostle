using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public Task<int> Te{caret}st()
        {
            return Task.FromResult(5);
        }
    }
}
