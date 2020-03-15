using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unify.PetStore.Test.Services.Contract
{
    public interface IPetService
    {
        /// <summary>
        /// Gets Pets by status and then groups them by category name
        /// </summary>
        /// <param name="status">pet status</param>
        /// <returns></returns>
        Task<List<IGrouping<string, Pet>>> GetCategorisedPetsByStatusAsync(Anonymous status);

        /// <summary>
        /// Sorts categorized pets descending by name and prints their name
        /// </summary>
        /// <param name="categorizedAvailablePets">grouped list of pets</param>
        void SortCategoryPetsByNameDescendingAndPrint(List<IGrouping<string, Pet>> categorizedAvailablePets);
    }
}