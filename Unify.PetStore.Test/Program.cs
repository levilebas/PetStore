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
                SortCategoryPetsByNameDescendingAndPrint(categorizedAvailablePets);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.Read();
        }

        internal static void SortCategoryPetsByNameDescendingAndPrint(List<IGrouping<string, Pet>> categorisedAvailablePets)
        {
            foreach (var petsGroupedByCategory in categorisedAvailablePets)
            {
                var categoryName = petsGroupedByCategory?.Key;
                Console.WriteLine(categoryName);
                if (petsGroupedByCategory == null || !petsGroupedByCategory.Any()) continue;
                foreach (var pet in petsGroupedByCategory.OrderByDescending(x => x.Name))
                {
                    Console.WriteLine($"--{pet.Name}");
                }
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
