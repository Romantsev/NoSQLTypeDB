using BLL.Interface;
using DAL.Interfaces;
using MongoDB.Bson;

namespace BLL.Services;
public class DocumentService : GenericService<BsonDocument>, IGenericService<BsonDocument>
{
    public DocumentService(IRepository<BsonDocument> repository) : base(repository)
    {
    }
}