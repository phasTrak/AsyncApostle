using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public void {caret}Test()
        {
            MethodAsync().Wait();
        }

        public async Task MethodAsync()
        {
        }
    }
}
