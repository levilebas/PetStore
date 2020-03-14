using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Unify.PetStore.Test.Services;
using Unify.PetStore.Test.Services.Contract;

namespace Unify.PetStore.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var serviceProvider = ConfigureServices();
                var petService = serviceProvider.GetService<IPetService>();

                var categorizedAvailablePets = await petService.GetCategorisedPetsByStatusAsync(Anonymous.Available);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(new HttpClient())
                .AddTransient<IPetStoreClient, PetStoreClient>()
                .AddTransient<IPetService, PetService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
