using AutoMapper;

namespace Capital.Placement.Api.Mapping;

public interface IMapWith<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}
