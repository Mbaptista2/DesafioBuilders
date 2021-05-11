using System;

namespace BST
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BinaryTree binaryTree = new();

            binaryTree.Add(1);
            binaryTree.Add(2);
            binaryTree.Add(3);
            binaryTree.Add(4);
            binaryTree.Add(5);

            //Busca de Node
            Node node = binaryTree.Find(2);

            NodeModel model = new();

            //Preparação do dado
            model.Id = Guid.NewGuid();
            model.Datas = binaryTree.GetAllNodes();

            Repository<NodeModel> repository = new Repository<NodeModel>(@"MyData.db");

            //Persistencia
            repository.Insert(model);

            //recuperação
            var teste = repository.Find(model.Id);

            //BinaryTree binaryTreeRecover = new(teste.Datas);
        }
    }
}