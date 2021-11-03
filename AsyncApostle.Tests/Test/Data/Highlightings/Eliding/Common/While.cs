﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync()
        {
            var i = 5;
            while (i > 0)
            {
                i--;
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}
