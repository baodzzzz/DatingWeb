using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);

    }
}