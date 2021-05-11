using Bogus;
using BST;
using System;
using System.Collections.Generic;

namespace TestBST._Builder
{
    public class NodeModelBuilder
    {
        protected Guid Id;
        protected List<int> Datas;

        public static NodeModel Novo(int DataSize)
        {
            var test = new Faker<NodeModel>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Datas, f => f.Make(DataSize, () => f.Random.Int(0, 10)));

            return test.Generate();
        }
    }
}