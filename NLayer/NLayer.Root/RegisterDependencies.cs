using Microsoft.Extensions.DependencyInjection;
using NLayer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Root
{
    public static class RegisterDependenciesExtention
    {
        public static void RegisterBusiness(this IServiceCollection services) {
            //var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(s => s.FullName.StartsWith("N"));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CustomerDto).Assembly));
        }

        public static void RegisterDomain(this IServiceCollection services) {

        }
    }
}
