using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unify.PetStore.Test.Services.Contract;

namespace Unify.PetStore.Test.Services
{
    public class PetService : IPetService
    {
        private readonly IPetStoreClient _petStoreClient;

        public PetService(IPetStoreClient petStoreClient)
        {
            _petStoreClient = petStoreClient;
        }

        public async Task<List<IGrouping<string, Pet>>> GetCategorisedPetsByStatusAsync(Anonymous status)
        {
            var findPetsByStatusResult = await _petStoreClient.FindPetsByStatusAsync(new List<Anonymous> { status });
            return findPetsByStatusResult
                .GroupBy(x => x.Category.Name)
                .ToList();
        }
    }
}