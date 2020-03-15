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
        private Mock<ILineWriterService> _mockLineWriterService;

        [SetUp]
        public void Setup()
        {
            _mockPetStoreClient = new Mock<IPetStoreClient>();
            _mockLineWriterService = new Mock<ILineWriterService>();
            _sut = new PetService(_mockPetStoreClient.Object, _mockLineWriterService.Object);
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
            Assert.AreEqual(1, result.First(x => x.Key == category2.ToString()).Count(), $"Category {category2} should contain 1 pet");
        }

        [Test]
        public async Task GetCategorisedPetsByStatusAsync_WhenPetsResponseIsEmpty_ShouldNotThrowError()
        {
            var response = new List<Pet>();

            _mockPetStoreClient.Setup(x => x.FindPetsByStatusAsync(new List<Anonymous> { Anonymous.Available }))
                .ReturnsAsync(response);

            var result = await _sut.GetCategorisedPetsByStatusAsync(Anonymous.Available);

            Assert.IsEmpty(result, "Categorized pets collection should be empty");
        }

        [Test]
        public void SortCategoryPetsByNameDescendingAndPrint_WhenValidCategorizedGroupData_ShouldSortByNameDescendingAndPrint()
        {
            var testData = new List<Pet>
            {
                CreatePet(PetStatus.Available, "Test Pet A", 1),
                CreatePet(PetStatus.Available, "Test Pet B", 2),
                CreatePet(PetStatus.Available, "Test Pet C", 1)
            }.GroupBy(x => x.Category.Name).ToList();

            _sut.SortCategoryPetsByNameDescendingAndPrint(testData);

            _mockLineWriterService.Verify(x => x.WriteLine("1"), Times.Once);
            _mockLineWriterService.Verify(x => x.WriteLine("--Test Pet C"), Times.Once);
            _mockLineWriterService.Verify(x => x.WriteLine("--Test Pet A"), Times.Once);
            _mockLineWriterService.Verify(x => x.WriteLine("2"), Times.Once);
            _mockLineWriterService.Verify(x => x.WriteLine("--Test Pet B"), Times.Once);
        }

        [Test]
        public void SortCategoryPetsByNameDescendingAndPrint_WhenCategorizedGroupCollectionIsEmpty_ShouldNotPrintAnything()
        {
            var testData = new List<Pet>
            {
                CreatePet(PetStatus.Available, "Test Pet A", 1),
                CreatePet(PetStatus.Available, "Test Pet B", 2),
                CreatePet(PetStatus.Available, "Test Pet C", 1)
            }.GroupBy(x => x.Category.Name).ToList();

            _sut.SortCategoryPetsByNameDescendingAndPrint(new List<IGrouping<string, Pet>>());

            _mockLineWriterService.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Never);
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