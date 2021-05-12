using Data.Repository;
using TestBST._Builder;
using TestBST.Fixture;
using Xunit;

namespace TestBST
{
    public class IntegrationTest : IClassFixture<DbFixture>
    {
        private readonly NodeRepository _repository;

        public IntegrationTest(DbFixture dbFixture)
        {
            _repository = new NodeRepository(dbFixture.connection, dbFixture.database);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        public void TestAddBD(int size)
        {
            var model = NodeModelBuilder.Novo(size);

            _repository.Insert(model);

            var result = _repository.Get(model.Id);

            Assert.NotNull(result);
            Assert.Equal(size, result.Datas.Count);
        }
    }
}