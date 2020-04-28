using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Solaris.Web.SolarApi.Infrastructure.Ioc
{
    public static class DependencyInjector
    {
        public static IServiceCollection Services { get; set; }
        private static List<Assembly> Assemblies { get; set; }
        private const string SOLUTION_NAME = "Solaris";

        static DependencyInjector()
        {
            Assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(t => t.FullName.Contains(SOLUTION_NAME))
                .ToList();
        }

        public static void InjectForNamespace(this IServiceCollection collection, string nameSpace)
        {
            InjectDependenciesForAssembly(Assemblies.FirstOrDefault(t => nameSpace.Contains(t.GetName().Name)), nameSpace);
        }

        private static void InjectDependenciesForAssembly(Assembly assembly, string nameSpace)
        {
            if (assembly == null)
                return;

            var typesToRegister = assembly.GetTypes()
                .Where(x => x.Namespace != null && x.Namespace.Equals(nameSpace, StringComparison.InvariantCultureIgnoreCase) && x.GetInterfaces().Any())
                .ToList();
            typesToRegister.ForEach(RegisterType);
        }

        private static void RegisterType(Type typeToRegister)
        {
            var registrationType = typeToRegister.GetCustomAttribute<RegistrationKindAttribute>();
            if (registrationType == null)
            {
                Services.AddSingleton(typeToRegister.GetInterfaces().First(), typeToRegister);
                return;
            }

            switch (registrationType.Type)
            {
                case RegistrationType.Singleton:
                    Services.AddSingleton(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
                case RegistrationType.Scoped:
                    Services.AddScoped(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
                case RegistrationType.Transient:
                    Services.AddTransient(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
                default:
                    Services.AddSingleton(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
            }
        }
    }
}