﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync(bool a)
        {
            if(a)
                return;

            await Task.Delay(1000).ConfigureAwait(false);
        }
    }
}