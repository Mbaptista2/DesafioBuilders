using BST.Data;
using System;
using System.Collections.Generic;

namespace BST
{
    public class NodeModel : IModel
    {
        public Guid Id { get; set; }
        public List<int> Datas { get; set; }
    }
}