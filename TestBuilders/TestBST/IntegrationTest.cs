using BST;
using TestBST._Builder;
using Xunit;

namespace TestBST
{
    public class IntegrationTest
    {
        private readonly Repository<NodeModel> _repository;

        public IntegrationTest()
        {
            _repository = new Repository<NodeModel>(@"test.db");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        public void TestAddBD(int size)
        {
            var model = NodeModelBuilder.Novo(size);

            _repository.Insert(model);

            var result = _repository.Find(model.Id);

            Assert.NotNull(result);
            Assert.Equal(size, result.Datas.Count);
        }
    }
}