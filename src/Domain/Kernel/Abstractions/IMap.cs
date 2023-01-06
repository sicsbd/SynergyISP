namespace SynergyISP.Domain.Abstractions;
using AutoMapper;

public interface IMap<T>
    where T : class
{
    public void Mapping(Profile profile)
        => profile.CreateMap(GetType(), typeof(T)).ReverseMap();
}
