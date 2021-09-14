using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeChat.Core.DataModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nIP Address: {IP}\n";
        }
    }
}
