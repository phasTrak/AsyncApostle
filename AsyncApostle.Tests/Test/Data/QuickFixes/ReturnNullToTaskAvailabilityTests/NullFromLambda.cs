using System;
using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public Func<Task> Test()
        {
            return () => null;
        }
    }
}
