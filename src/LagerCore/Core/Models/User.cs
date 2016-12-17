using LagerCore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagerCore.Core.Models
{
    public sealed class User : IUser
    {
        public string Password { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public bool Admin { get; set; }

        public bool IsActive { get; set; }


        public bool IsAdmin()
        {
            return Admin;
        }
    }
}
