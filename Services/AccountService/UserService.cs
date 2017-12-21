using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.DTO.AccountDTO;
using Common.DTO.Communication;
using Common.DTO.TokenDTO;
using Common.Entity;
using Common.Enum;
using Common.Helper;
using Common.Interfaces.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using IUser = Common.Interfaces.Entity.IUser;

namespace Services.AccountService
{
   public class UserService:IUserService
   {

       private readonly MSContext _context;
   

        public UserService(MSContext context)
        {
            _context = context;
           
        }

        public async Task<Response<OperationResultEnum>> CreateAccount(CreateAccount createAccount)
        {
            var response = new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };

            try
            {
                if (createAccount.Nike == null)
                {
                    response.Error = new Error(400, "Missed Nike");
                    return response;
                }
                if (string.IsNullOrEmpty(createAccount.Password))
                {
                    response.Error = new Error(400, "Missed Password");
                    return response;
                }
                if (string.IsNullOrEmpty(createAccount.ConfirmPassword))
                {
                    response.Error = new Error(400, "Missed Confirm Password");
                    return response;
                }

                var isNikeAlreadyUsed = await _context.Users.Where(p => p.Nike==createAccount.Nike).FirstOrDefaultAsync();
                if (isNikeAlreadyUsed!=null)
                {
                    response.Error=new Error(400,"This Nike is already used!");
                    return response;
                }
                if (createAccount.Password!=createAccount.ConfirmPassword)
                {
                    response.Error=new Error(400,"Confirm password is not equals with password!");
                    return response;
                }
                UserRole role =UserRole.User;
                var user = new UserEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Nike = createAccount.Nike,
                    Password = createAccount.Password,
                    Role = role
                    
                };
                await _context.Users.AddAsync(user);
                var confirmationToken = Guid.NewGuid().ToString();
                await _context.Tokens.AddAsync(new TokenEntity
                {
                    UsserId = user.Id,
                    ExpirationDateTime = DateTime.UtcNow.AddDays(1),
                    Id = confirmationToken
                });

               await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;
            }
            catch (Exception ex)
            {
           
                response.Error=new Error(500,"Can`t register new User: "+ex);
            }
            return response;
        }

       public async Task<Response<ClaimsIdentity>> LogIn(LogInAccount logIn)
       {
           var response = new Response<ClaimsIdentity>();
           try
           {
               var user = await GetUserByCreds(logIn.Nike,logIn.Password, logIn.UserRole);
               if (user == null)
               {
                   response.Error = new Error(404, "User not found!");
                   return response;
               }
                var isCorrectDataFromUser =
                   await _context.Users.AnyAsync(p => p.Nike == logIn.Nike && p.Password == logIn.Password);
               if (!isCorrectDataFromUser)
               {
                   response.Error = new Error(400, "Nike or password is wrong");
                   return response;
               }

               var claims=new List<Claim>
               {
                   new Claim(ClaimsIdentity.DefaultNameClaimType,user.Id.ToString()),
                   new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.ToString())
               };
                ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,"Token",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
               response.Data = claimsIdentity;
               return response;
           }
           catch (Exception ex)
           {
               
                response.Error=new Error(500,"Can`t LogIn: "+ex);
               return response;
           }

       }

       public async Task<Response<UserInfoDTO>> GetCurrentUserInfo(string userId)
       {
          var response=new Response<UserInfoDTO>();

           try
           {
               var user = await _context.Users.Where(p => p.Id == userId).FirstOrDefaultAsync();
               if (user==null)
               {
                    response.Error=new Error(404,"User not found!");
                   return response;
               }

               response.Data=new UserInfoDTO(user.Nike,user.Role);
               
           }
           catch (Exception e)
           {
               response.Error = new Error(500, "Can`t get current user info: " + e);
            }


           return response;
       }

       public async Task<IUser> GetUserByCreds(string userNike,string password,UserRole userRole)
       {
           var encrypt = password;
           var user = await _context.Users.FirstOrDefaultAsync(p => p.Nike == userNike && p.Password == encrypt&&p.Role==userRole);
           return user;
       }

       public async Task<Response<TokenDTO>> GetToken(ClaimsIdentity identity)
       {
            Response<TokenDTO> response=new Response<TokenDTO>();
           try
           {
               var now = DateTime.UtcNow;
               var token = new JwtSecurityToken(
                   issuer:AutoOptions.AutoOptions.ISSUER,
                   audience:AutoOptions.AutoOptions.AUDIENS,
                   notBefore:now,
                   claims:identity.Claims,
                   expires:now.Add(TimeSpan.FromMinutes(AutoOptions.AutoOptions.LIFETIME)),
                   signingCredentials:new SigningCredentials(AutoOptions.AutoOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256));
               var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
               var tokenResponse = new TokenDTO()
               {
                   TokenString = encodedJwt,
                   Role = identity.Claims?.FirstOrDefault(p=>p.Type==ClaimsIdentity.DefaultRoleClaimType)?.Value,
                   UserId = identity.Name

               };

               response.Data = tokenResponse;
               return response;
           }
           catch (Exception ex)
           {
            
                response.Error=new Error(500,"Server Error: "+ex);
               return response;
           }
       }

       public async Task<Response<IUser>> GetUserInfo(string UserId)
       {
            Response<IUser> response=new Response<IUser>();
           try
           {
               if (UserId==null)
               {
                    response.Error=new Error(400,"Missed User Id!");
                   return response;
               }
               var user = await _context.Users.Where(p => p.Id == UserId).FirstOrDefaultAsync();
               if (user==null)
               {
                   response.Error=new Error(404,"Can`t find user");
                   return response;

               }
               response.Data = user;
           }
           catch (Exception e)
           {
         
              response.Error=new Error(500,"Can`t get user profile: "+e);
           }
           return response;
       }

       public async Task<Response<ICollection<IUser>>> GetAllUsers()
       {
           var response=new Response<ICollection<IUser>>();

           try
           {
               var userList = await _context.Users.ToListAsync();

                var responseUserList=new List<IUser>();

               foreach (var item in userList)
               {
                   responseUserList.Add(item);
               }

               response.Data = responseUserList;
                

           }
           catch (Exception e)
           {
               response.Error = new Error(500, "Can`t get users: " + e);
            }


           return response;
       }


       //Method need to change

        public async Task<Response<OperationResultEnum>> LogOut(string UserId)
        {
            var response=new Response<OperationResultEnum>()
            {
                Data = OperationResultEnum.Fail
            };

            try
            {
                if (UserId == null)
                {
                    response.Error = new Error(400, "Missed User Id!");
                    return response;
                }
                var token = await _context.Tokens.Where(p => p.User.Id == UserId).FirstOrDefaultAsync();
               
                if (token == null)
                {
                    response.Error = new Error(404, "Can`t find user");
                    return response;

                }

                _context.Tokens.Remove(token);

                await _context.SaveChangesAsync();

                response.Data = OperationResultEnum.Success;


            }
            catch (Exception e)
            {
                response.Error = new Error(500, "Can`t get user profile: " + e);
            }

            return response;
        }
    }
}
