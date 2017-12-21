using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO.Communication;

using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.QuestionService;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController:Controller
    {



    }
}
