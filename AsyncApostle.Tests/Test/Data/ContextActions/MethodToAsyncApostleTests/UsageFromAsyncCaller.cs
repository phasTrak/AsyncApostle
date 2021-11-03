using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public int {caret}Test()
        {
            return 5;
        }
        public async Task Caller()
        {
            var a = Test();
        }
    }
}
