using AutoMapper;
using BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyAssemblyMapping(assembly);
        }

        private void ApplyAssemblyMapping(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                        .Where(t => t.GetInterfaces()
                            .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMapWith<>))
                        )
                        .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("UseMap");
                methodInfo?.Invoke(instance, new object[]{ this });
            }
        }
    }
}
