using Data.Interfaces;
using System.Collections.Generic;

namespace Data
{
    public class NodeModel : IEntity
    {
        public string Id { get; set; }
        public List<int> Datas { get; set; }
    }
}