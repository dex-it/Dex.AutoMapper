using System;

namespace AutoMapper.Extensions.ExpressionMapping;

public static class ConfigurationHelper
{
    public static MapperConfiguration GetMapperConfiguration(Action<IMapperConfigurationExpression> configure) => new(configure);
    public static MapperConfiguration GetMapperConfiguration(MapperConfigurationExpression configurationExpression) => new(configurationExpression);
}