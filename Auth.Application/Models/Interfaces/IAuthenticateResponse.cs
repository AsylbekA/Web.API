using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Models.Interfaces
{
    public interface IAuthenticateResponse
    {
         int Id { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
         string Username { get; set; }
         string Token { get; set; }
    }
}
