using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLayer.Business.Models;
using NLayer.Core.Models;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;
using NLayer.Infrastructure;
using NLayer.Infrastructure.Repositories;

namespace NLayer.Root
{
    public static class RegisterDependenciesExtention
    {
        public static void RegisterBusiness(this IServiceCollection services) {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CustomerDto).Assembly));
        }

        public static void RegisterDomain(this IServiceCollection services) {
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IRepository<Item>, Repository<Item>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterInfrastructure(this IServiceCollection services, DatabaseOptions dbOptions = null) {
            services.AddDbContext<ShopContext>(
                options => options.UseSqlServer(dbOptions.ConnectionString, providerOptions => {
                    providerOptions.EnableRetryOnFailure(dbOptions.RetryCount);
                    providerOptions.CommandTimeout(dbOptions.OperationTimeout);
            }));
        }
    }
}
