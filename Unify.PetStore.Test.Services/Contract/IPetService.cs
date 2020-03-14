using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unify.PetStore.Test.Services.Contract
{
    public interface IPetService
    {
        Task<List<IGrouping<string, Pet>>> GetCategorisedPetsByStatusAsync(Anonymous status);
    }
}