using MongoDB.Bson.Serialization.Attributes;

namespace Data.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// id
        /// </summary>
        [BsonId]
        string Id { get; set; }
    }
}