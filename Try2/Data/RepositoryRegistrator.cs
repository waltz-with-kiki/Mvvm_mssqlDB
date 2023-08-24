using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;
using Try2.Interfaces;

namespace Try2.Data
{
    public static class RepositoryRegistrator
    {

        public static IServiceCollection AddRepositoryInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Right>, DbRepository<Right>>()
            .AddTransient<IRepository<Bank>, DbRepository<Bank>>()
            .AddTransient<IRepository<Checking_account>, Checking_accountsRepository>()
            .AddTransient<IRepository<Client>, ClientsRepository>()
            .AddTransient<IRepository<Crew>, DbRepository<Crew>>()
            .AddTransient<IRepository<Brand>, DbRepository<Brand>>()
            .AddTransient<IRepository<Automobile>, AutomobilesRepository>()
            .AddTransient<IRepository<DriverCategory>, DbRepository<DriverCategory>>()
            .AddTransient<IRepository<DriverClass>, DbRepository<DriverClass>>()
            .AddTransient<IRepository<Driver>, DriversRepository>()
            .AddTransient<IRepository<Flight>, FlightsRepository>()
            .AddTransient<IRepository<Order>, OrdersRepository>()
            .AddTransient<IRepository<Unit>, DbRepository<Unit>>()
            .AddTransient<IRepository<Cargo>, CargosRepository>()
            ;
    }
}
