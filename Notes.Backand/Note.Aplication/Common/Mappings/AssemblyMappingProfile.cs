
using System.Reflection;

using AutoMapper;

namespace Notes.Aplication.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile ( Assembly assembly )
        {
            ApplyMappingsFromAssebly (assembly);
        }

        private void ApplyMappingsFromAssebly ( Assembly assembly )
        {
            var assemblyNames = assembly.GetExportedTypes ( )
                .Where (t => t.GetInterfaces ( ).
                Any (i => i.IsGenericType&&i.GetGenericTypeDefinition ( )==typeof (IMapWiht<>))).ToList ( );

            foreach ( var type in assemblyNames)
            {
                var instsnse = Activator.CreateInstance (type);
                var methodInfo = type.GetMethod ("Mapping" );
                methodInfo?.Invoke (instsnse,new object[] {this} );
            }
        }
    }
}
