using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync()
        {
            var tasks = new Task[0];
            foreach(var task in tasks)
            {
                await task.ConfigureAwait(false);
            }
        }
    }
}
