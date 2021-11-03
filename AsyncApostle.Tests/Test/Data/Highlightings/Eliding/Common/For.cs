﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync()
        {
            for (var i = 0; i < 5; i++)
            {
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}
