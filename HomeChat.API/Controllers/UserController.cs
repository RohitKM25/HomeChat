using HomeChat.Core.DataAccess;
using HomeChat.Core.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeChat.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await DataAccess.Users.ReadUsers();
        }

        [HttpPost]
        [Route("add")]
        public async Task<bool> AddUserAsync(string name,string ip)
        {
            return await DataAccess.Users.AddUser(new UserModel() { Name = name, IP = ip });
        }

        [HttpGet]
        [Route("filterbyid")]
        public async Task<IEnumerable<UserModel>> FilterUserAsync(string id)
        {
            return await DataAccess.Users.FilterUsers("Id", id);
        }

        [HttpPost]
        [Route("update")]
        public async Task<bool> UpdateUserAsync(string columnName,string type, string value,string id)
        {
            var dataType = type == "int" ? typeof(int) : typeof(string);
            var columns = new List<(string columnName, Type dataType, string value)>() {
                        (columnName, dataType, value),
                    };
            return await DataAccess.Users.UpdateUser(columns, id);
        }

        [HttpGet]
        [Route("sent")]
        public async Task<IEnumerable<MessageModel>> ReadSentMessagesAsync(string id)
        {
            return await DataAccess.Messages.ReadUserSentMessages(id);
        }

        [HttpGet]
        [Route("received")]
        public async Task<IEnumerable<MessageModel>> ReadReceivedMessagesAsync(string id)
        {
            return await DataAccess.Messages.ReadUserReceivedMessages(id);
        }

        [HttpPost]
        [Route("send")]
        public async Task<bool> SendNewMessage(JObject jsonResult)
        {
            MessageModel item = JsonConvert.DeserializeObject<MessageModel>(jsonResult.ToString());
            return await DataAccess.Messages.AddMessage(item);
        }
    }
}
