using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Unify.PetStore.Test.Services;
using Unify.PetStore.Test.Services.Contract;

namespace Unify.PetStore.Test.UnitTests.Services
{
    public class PetServiceTests
    {
        private PetService _sut;
        private Mock<IPetStoreClient> _mockPetStoreClient;

        [SetUp]
        public void Setup()
        {
            _mockPetStoreClient = new Mock<IPetStoreClient>();
            _sut = new PetService(_mockPetStoreClient.Object);
        }

        [Test]
        public async Task GetCategorisedPetsByStatusAsync_WhenPetsHaveSameCategory_ShouldSortIntoCategories()
        {
            var category1 = 1;
            var category2 = 2;

            var response = new List<Pet>
            {
                CreatePet(PetStatus.Available, "Test Pet A", category1),
                CreatePet(PetStatus.Available, "Test Pet B", category2),
                CreatePet(PetStatus.Available, "Test Pet C", category1)
            };

            _mockPetStoreClient.Setup(x => x.FindPetsByStatusAsync(new List<Anonymous> { Anonymous.Available }))
                .ReturnsAsync(response);

            var result = await _sut.GetCategorisedPetsByStatusAsync(Anonymous.Available);

            Assert.AreEqual(2, result.First(x => x.Key == category1.ToString()).Count(), $"Category {category1} should contain 2 pets");
        }

        private Pet CreatePet(PetStatus petStatus, string petName, int category)
        {
            return new Pet
            {
                Name = petName,
                Status = petStatus,
                Category = new Category
                {
                    Name = category.ToString(),
                    Id = category
                }
            };
        }
    }
}