﻿using System.Threading.Tasks;
using LibraryToOverride;

namespace AsyncApostle.Tests
{
    public class Class : ClassToOverride
    {
        protected override Task Method()
        {
            return Task.CompletedTask;
        }
    }
}