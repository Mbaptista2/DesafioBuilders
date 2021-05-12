using Data;
using Data.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BST
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

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
            model.Id = Guid.NewGuid().ToString();
            model.Datas = binaryTree.GetAllNodes();

            NodeRepository repository = new NodeRepository(configuration["ConnectionString"], configuration["DatabaseName"]);

            //Persistencia
            repository.Insert(model);

            //recuperação
            var entidadeSalva = repository.Get(model.Id);

            //BinaryTree binaryTreeRecover = new(entidadeSalva.Datas);
        }
    }
}