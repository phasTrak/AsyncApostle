﻿using System.Threading.Tasks;

namespace AsyncApostle.Tests.Test.Data.FixReturnValueToTaskTests
{
    using Microsoft.AspNetCore.Mvc;
    public class Class : Controller
    {
        public async Task Test()
        {
            await Task.Delay(1000).ConfigureAwait(false);
        }
    }
}

namespace Microsoft.AspNetCore.Mvc
{
    public class Controller
    { }
}