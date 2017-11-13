using Microsoft.Extensions.Configuration;

namespace Ericore.Services
{
    public interface IDBTag
    {
        string GetDBTag();
    }
    public class DBTag : IDBTag
    {
        private string _DBTag;

        public DBTag(IConfiguration configuration)
        {
            _DBTag = configuration["database:connectionString"];
        }
        public string GetDBTag()
        {
            return _DBTag;
        }
    }
}
