using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public void {caret}Test()
        {
            var a = MethodAsync().Result;
        }

        public async Task<int> MethodAsync()
        {
            return 5;
        }
    }
}
