using Data.Repository.Base;

namespace Data.Repository
{
    public class NodeRepository : Repository<NodeModel>
    {
        public NodeRepository(string connectionString, string dataBaseName) : base(connectionString, dataBaseName, typeof(NodeModel).Name)
        {
        }
    }
}