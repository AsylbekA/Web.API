using Auth.Application.Models.Interfaces;
using Auth.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Models
{
    public class AuthenticateResponseImp : IAuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseImp(Login user, string token)
        {
            Id = user.Id;
            FirstName = "Anarbay"; //  user.Users.FirstName;
            LastName = "Asylbek"; // user.Users.LastName;
            Username = "Kuatbekyly"; // user.Users.Username;
            Token = token;
        }
    }
}
