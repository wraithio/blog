using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class UserModel
    {
        public int Id {get;set;}
        public string? Username {get;set;}
        public string? Salt {get;set;}
        public string? Hash {get;set;}
    }
}