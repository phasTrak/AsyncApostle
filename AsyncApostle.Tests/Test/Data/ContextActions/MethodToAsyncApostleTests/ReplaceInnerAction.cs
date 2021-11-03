using System;
using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public void {caret}Test()
        {
            Method(() => InnerMethod());
        }

        public void Method(Action func)
        {
            func();
        }

        public Task MethodAsync(Func<Task> func)
        {
            return func();
        }

        public void InnerMethod()
        {
        }

        public Task InnerMethodAsync()
        {
            return Task.CompletedTask;
        }
    }
}
