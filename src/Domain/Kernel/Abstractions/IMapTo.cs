using AutoMapper;

namespace SynergyISP.Domain.Abstractions;
public interface IMapTo<T>
    where T : class
{
    public void Mapping(Profile profile)
        => profile.CreateMap(GetType(), typeof(T));
}
