namespace SynergyISP.Application.Common.Mappings; using System.Linq.Expressions;
using AutoMapper;

internal class MapperConfigurationProvider
    : IConfigurationProvider
{
    private readonly MapperConfiguration _mapperConfiguration;

    public MapperConfigurationProvider(
        MapperConfiguration mapperConfiguration)
    {
        _mapperConfiguration = mapperConfiguration;
    }

    public void AssertConfigurationIsValid()
    {
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    /// <inheritdoc/>
    public LambdaExpression BuildExecutionPlan(Type sourceType, Type destinationType)
    {
        return _mapperConfiguration.BuildExecutionPlan(sourceType, destinationType);
    }

    /// <inheritdoc/>
    public void CompileMappings()
    {
        _mapperConfiguration.CompileMappings();
    }

    /// <inheritdoc/>
    public IMapper CreateMapper()
    {
        IMapper mapper = _mapperConfiguration.CreateMapper();
        return mapper;
    }

    /// <inheritdoc/>
    public IMapper CreateMapper(Func<Type, object> serviceCtor)
    {
        IMapper mapper = _mapperConfiguration.CreateMapper(serviceCtor);
        return mapper;
    }
}
