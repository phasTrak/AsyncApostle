﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public int {caret}TestAsync()
        {
            return 5;
        }
    }

    public class Class1
    {
        public int Method()
        {
            var @class = new Class();
            var a = @class.TestAsync();
        }
    }
}
