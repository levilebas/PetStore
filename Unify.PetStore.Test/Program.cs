using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Unify.PetStore.Test.Services;

namespace Unify.PetStore.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(new HttpClient())
                .AddTransient<IPetStoreClient, PetStoreClient>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
