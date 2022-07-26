
using AutoMapper;

namespace Notes.Aplication.Common.Mappings
{
    public interface IMapWiht<T>
    {
        void Mapping ( Profile profile ) =>
            profile.CreateMap (typeof (T), GetType ( ));
    }
}
