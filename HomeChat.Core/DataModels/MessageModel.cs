using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeChat.Core.DataModels
{
    public class MessageModel
    {
        public int Id {  get; set; }
        [JsonProperty]
        public int SenderId {  get; set; }
        [JsonProperty]
        public int ReceiverId { get; set; }
        [JsonProperty]
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }=DateTime.Now;
        public int IsRead { get; set; } = 0;

        public override string ToString()
        {
            return $"Id: {Id}\nSenderId: {SenderId}\nReceiverId: {ReceiverId}\nDate Time: {TimeStamp}\nIs Read: {IsRead!=0}\nContent:\n {Content}\n";
        }
    }
}
