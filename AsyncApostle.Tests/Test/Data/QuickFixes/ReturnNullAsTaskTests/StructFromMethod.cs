﻿using System.Collections;
using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public Task<int> Test()
        {
            return {caret}null;
        }
    }
}