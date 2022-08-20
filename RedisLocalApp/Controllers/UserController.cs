using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Redis.OM;
using Redis.OM.Searching;
using RedisLocalApp.Models;
using System.Text;
using System.Text.Json;

namespace RedisLocalApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        //private readonly IDistributedCache _cache;
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<User> _users;


        public UserController(ILogger<UserController> logger,
            RedisConnectionProvider provider
            //IDistributedCache cache
            )
        {
            _provider = provider;
            _logger = logger;
            _users = (RedisCollection<User>)provider.RedisCollection<User>();
            //_cache = cache;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<User> GetUser(int id)
        {
            try
            {
                string cacheKey = id.ToString();

                User user = await _users.FindByIdAsync(id.ToString());

                return user;
                //if (user != null)
                //{
                //    var cachedDataString = Encoding.UTF8.GetString(cachedData);
                //    return JsonSerializer.Deserialize<User>(cachedDataString);
                //}

                //return null;
            }catch(Exception e)
            {
                _logger.LogError("error ", e);
                throw new Exception("error ", e);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<string> SetUser([FromBody] User user)
        {
            try
            {
                return await _users.InsertAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError("error ", e);
                throw new Exception("error ", e);
            }
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<User> GetUser(int id)
        //{
        //    try
        //    {
        //        string cacheKey = id.ToString();

        //        byte[] cachedData = await _cache.GetAsync(cacheKey);

        //        if (cachedData != null)
        //        {
        //            var cachedDataString = Encoding.UTF8.GetString(cachedData);
        //            return JsonSerializer.Deserialize<User>(cachedDataString);
        //        }

        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError("error ", e);
        //        throw new Exception("error ", e);
        //    }
        //}

        //[HttpPost]
        //[Route("create")]
        //public async Task SetUser([FromBody] User user)
        //{
        //    try
        //    {
        //        string cachedDataString = JsonSerializer.Serialize(user);
        //        var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

        //        await _cache.SetAsync("4", dataToCache);

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError("error ", e);
        //        throw new Exception("error ", e);
        //    }
        //}
    }
}