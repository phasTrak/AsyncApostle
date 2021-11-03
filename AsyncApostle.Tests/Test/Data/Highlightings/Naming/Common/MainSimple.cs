using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public Task Main()
        {
            return Task.CompletedTask;
        }
    }
}
