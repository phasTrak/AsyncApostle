using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public Task Test()
        {
            return Task.CompletedTask;
        }
    }
}
