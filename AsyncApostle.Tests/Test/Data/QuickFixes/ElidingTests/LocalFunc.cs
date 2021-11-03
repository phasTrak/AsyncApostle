using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync()
        {
            await Foo().ConfigureAwait(false);
            async Task Foo()
            {
                {caret}await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}
