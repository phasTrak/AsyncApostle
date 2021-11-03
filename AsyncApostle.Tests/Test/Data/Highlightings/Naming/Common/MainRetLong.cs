using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public Task<long> Main()
        {
            return Task.FromResult(0L);
        }
    }
}
