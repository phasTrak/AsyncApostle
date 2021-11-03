﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task SomeFuncAsync(bool foo)
        {
            if (foo)
                await BarAsync().ConfigureAwait(false);
        }

        public Task BarAsync()
        {
            return Task.CompletedTask;
        }
    }
}
