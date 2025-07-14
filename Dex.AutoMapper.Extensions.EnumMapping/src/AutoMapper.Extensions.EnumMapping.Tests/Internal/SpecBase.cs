﻿using System;

namespace AutoMapper.Extensions.EnumMapping.Tests.Internal;

public class SpecBase : SpecBaseBase, IDisposable
{
    private protected SpecBase()
    {
        Establish_context();
        Because_of();
    }

    public void Dispose()
    {
        Cleanup();
    }
}