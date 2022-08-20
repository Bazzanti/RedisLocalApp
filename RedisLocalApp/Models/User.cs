using Redis.OM.Modeling;

namespace RedisLocalApp.Models
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "user" })]
    public class User
    {
        [RedisIdField]
        public int id { get; set; }
        [Indexed]
        public string? name { get; set; }
        [Indexed]
        public string? surname { get; set; }
    }
}
