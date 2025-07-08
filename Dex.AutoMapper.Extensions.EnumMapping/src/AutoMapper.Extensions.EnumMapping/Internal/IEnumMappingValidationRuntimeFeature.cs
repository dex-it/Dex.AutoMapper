﻿using AutoMapper.Configuration;
using AutoMapper.Features;

namespace AutoMapper.Extensions.EnumMapping.Internal;

internal interface IEnumMappingValidationRuntimeFeature : IRuntimeFeature
{
    void Validate(ValidationContext validationContext);
}