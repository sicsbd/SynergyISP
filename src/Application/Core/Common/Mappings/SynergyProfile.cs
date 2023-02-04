
using System.Reflection;
using AutoMapper;
using SynergyISP.Domain.Abstractions;

namespace SynergyISP.Application.Common.Mappings;
internal class SynergyProfile : Profile
{
    private const string MethodName = "Mapping";
    private const string MapFromName = "IMapFrom";
    private const string MapToName = "IMapTo";
    private const string MapName = "IMap";

    /// <summary>
    /// Initializes a new instance of the <see cref="SynergyProfile"/> class.
    /// </summary>
    /// <param name="assemblies">The assemblies where the framework should search for implemation of <see cref="IMap{T}"/>, <see cref="IMapFrom{T}"/> and <see cref="IMapTo{T}"/>.</param>
    public SynergyProfile(params Assembly[] assemblies)
    {
        IEnumerable<Assembly> scanAssemblies = assemblies.Concat(new[]
        {
            Assembly.GetExecutingAssembly(),
            Assembly.GetCallingAssembly(),
        });
        ApplyMappingsFromAssembly(scanAssemblies);
    }

    private void ApplyMappingsFromAssembly(IEnumerable<Assembly> assemblies)
    {
        var exportedTypes = assemblies.SelectMany(a => a.GetExportedTypes());
        var iMapFromTypes = exportedTypes
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        var iMapToTypes = exportedTypes
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
            .ToList();

        var iMapTypes = exportedTypes
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMap<>)))
            .ToList();

        Map(iMapFromTypes, MapFromName);
        Map(iMapToTypes, MapToName);
        Map(iMapTypes, MapName);
    }

    private void Map(IEnumerable<Type> types, string interfaceName)
    {
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfos = type
                                .GetInterfaces()
                                .Where(i => i.Name.StartsWith($"{interfaceName}`1"))
                                .Select(i => i.GetMethod(MethodName));
            methodInfos.ToList().ForEach(methodInfo
                => methodInfo?.Invoke(instance, new object[] { this }));
        }
    }

}
