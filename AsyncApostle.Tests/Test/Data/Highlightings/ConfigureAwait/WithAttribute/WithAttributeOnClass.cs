using System;
using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.Highlightings.ConfigureAwaitWithAttribute
{
    [MyCustom]
    public class Class
    {
        public async Task TestAsync()
        {
            await Task.Delay(1000);
        }
    }

    public class MyCustomAttribute : Attribute
    {

    }
}
