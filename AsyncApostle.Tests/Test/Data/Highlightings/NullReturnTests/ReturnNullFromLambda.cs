using System;
using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public void Test()
        {
            Test2(() => null);
        }

        public void Test2(Func<Task> func)
        {
            func();
        }
    }
}
