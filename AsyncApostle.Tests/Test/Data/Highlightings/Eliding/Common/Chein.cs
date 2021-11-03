﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    public class Class
    {
        public async Task TestAsync()
        {
            (await MethodAsync().ConfigureAwait(false)).Method();
        }

        public Task<Class> MethodAsync()
        {
            return Task.FromResult<Class>(null);
        }

        public void Method()
        {
        }
    }
}
