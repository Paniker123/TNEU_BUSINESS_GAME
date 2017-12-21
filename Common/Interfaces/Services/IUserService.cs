using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.AccountDTO;
using Common.DTO.Communication;
using Common.DTO.TokenDTO;
using Common.Enum;
using Common.Interfaces.Entity;


namespace Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<OperationResultEnum>> CreateAccount(CreateAccount createAccount);
        Task<Response<ClaimsIdentity>> LogIn(LogInAccount logIn);
        Task<Response<UserInfoDTO>> GetCurrentUserInfo(string userId);
        Task<IUser> GetUserByCreds(string userNike, string password, UserRole userRole);
        Task<Response<TokenDTO>> GetToken(ClaimsIdentity identity);
        Task<Response<OperationResultEnum>> LogOut(string userId);


        Task<Response<IUser>> GetUserInfo(string userId);
        Task<Response<ICollection<IUser>>> GetAllUsers();

    }
}
