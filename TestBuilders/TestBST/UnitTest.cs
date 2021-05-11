using BST;
using Xunit;

namespace TestBST
{
    public class UnitTest
    {
        private readonly BinaryTree _binaryTree;

        public UnitTest()
        {
            _binaryTree = new BinaryTree();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        public void TestAdd(int size)
        {
            for (int i = 0; i < size; i++)
            {
                _binaryTree.Add(i);
            }

            Assert.Equal(size, _binaryTree.GetTreeDepth());
        }

        [Fact]
        public void TestFind()
        {
            for (int i = 0; i < 3; i++)
            {
                _binaryTree.Add(i);
            }

            var node = _binaryTree.Find(1);

            Assert.NotNull(node);
        }
    }
}