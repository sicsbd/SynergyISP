using AutoMapper;

namespace SynergyISP.Domain.Abstractions;
public interface IMapFrom<T>
    where T : class
{
    public void Mapping(Profile profile)
        => profile.CreateMap(typeof(T), GetType());
}
