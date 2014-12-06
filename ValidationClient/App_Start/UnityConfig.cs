using System.Collections.Generic;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace ValidationClient
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //Auto-register
            var refs = BuildManager.GetReferencedAssemblies();
            var aa = new Assembly[refs.Count];
            refs.CopyTo(aa, 0);
            var all = new List<Assembly>(aa);

            //Register Business, Shared, and Service
            var libs = all.FindAll(a => a.FullName.StartsWith("Address"));
            var resources = all.FindAll(a => a.FullName.StartsWith("ResourceAccess"));
            var models = all.FindAll(a => a.FullName.StartsWith("Models"));
            var data = all.FindAll(a => a.FullName.StartsWith("Data"));

            container.RegisterTypes(
                AllClasses.FromAssemblies(libs),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);

            container.RegisterTypes(
                AllClasses.FromAssemblies(resources),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);

            container.RegisterTypes(
                AllClasses.FromAssemblies(models),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
            container.RegisterTypes(
                AllClasses.FromAssemblies(data),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}