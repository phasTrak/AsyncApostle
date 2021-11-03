using System.Threading.Tasks;
using LibraryToOverride;

namespace AsyncApostle.Tests
{
    public class Class : IInterfaceToOverride
    {
        public Task Method()
        {
            return Task.CompletedTask;
        }
    }
}