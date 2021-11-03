﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task<object> TestAsync()
        {
            await Test2Async().ConfigureAwait(false);
            return null;
        }

        public Task Test2Async()
        {
            return Task.CompletedTask;
        }
    }
}
