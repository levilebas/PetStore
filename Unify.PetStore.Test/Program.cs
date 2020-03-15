using System;
using System.Collections.Generic;
using System.Linq;
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
                if (!categorizedAvailablePets.Any())
                {
                    Console.WriteLine("There are no pets to display!");
                }
                else
                {
                    petService.SortCategoryPetsByNameDescendingAndPrint(categorizedAvailablePets);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.Read();
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(new HttpClient())
                .AddTransient<ILineWriterService, ConsoleLineWriterService>()
                .AddTransient<IPetStoreClient, PetStoreClient>()
                .AddTransient<IPetService, PetService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
