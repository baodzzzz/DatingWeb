using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected readonly DatingAppContext _context;
        protected readonly ITokenService _tokenService;

        public BaseApiController(DatingAppContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }


    }
}