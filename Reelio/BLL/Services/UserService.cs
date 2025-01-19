using BLL.Helpers;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Common.DTO.WatchList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly JWTHelper jwtHelper;
        private readonly IConfiguration _configuration;



        public UserService(IUserRepository iuserRepository, IConfiguration configuration)
        {
            userRepository = iuserRepository;
            _configuration = configuration;
            jwtHelper = new JWTHelper(_configuration["JWTSECRET"]);
        }
        public async Task<string?> RegisterUser(UserDTO user)
        {

            user.Password = HashHelper.Encrypt(user.Password);
            Guid newUserId = Guid.NewGuid();

            UserDTO userDTO = new UserDTO
            {
                Id = newUserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            await userRepository.AddUser(userDTO);
            return jwtHelper.GenerateToken(new UserJWTDTO(userDTO.Id));
        }


        public async Task<ResponseDTO> Authenticate(LoginDTO body)
        {
            if (body == null)
            {
                return new ResponseDTO
                {
                    Message = "Invalid request",
                };
            }

            UserDTO user = await userRepository.GetUserByEmail(body.Email);
            if (user == null || !HashHelper.Verify(body.Password, user.Password))
            {
                return new ResponseDTO
                {
                    Message = "Invalid credentials",
                };
            }

            UserJWTDTO userJWT = new UserJWTDTO(user.Id );

            return new ResponseDTO
            {
                Token = jwtHelper.GenerateToken(userJWT),
                Message = "Success"
            };
        }

        public UserJWTDTO? ValidateToken(string token)
        {
           return jwtHelper.ValidateToken(token);
        }


        public List<WatchListDTO> GetWatchlist(Guid userId)
        {
            var items = userRepository.GetWatchlist(userId);

            return items.Select(item => new WatchListDTO
            {
                Id = item.Id,
                ContentId = item.ContentId,
                ContentType = item.ContentType,
                Status = item.Status,
                Rating = item.Rating,
                Review = item.Review
            }).ToList();
        }

        public WatchListDTO AddToWatchlist(Guid userId, CreateWatchlistDTO dto)
        {
            if (dto.ContentType.ToLower() != "movie" && dto.ContentType.ToLower() != "show")
            {
                throw new ArgumentException("Invalid content type. Must be 'movie' or 'show'.");
            }

            var watchlistEntity = userRepository.AddToWatchlist(userId, dto);

            return new WatchListDTO
            {
                Id = watchlistEntity.Id,
                ContentId = watchlistEntity.ContentId,
                ContentType = watchlistEntity.ContentType,
                Status = watchlistEntity.Status,
                Rating = watchlistEntity.Rating,
                Review = watchlistEntity.Review,
            };
        }

        public async Task<bool> RemoveFromWatchlist(Guid userId, string watchlistItemId, string contentType)
        {
            return await userRepository.RemoveFromWatchlistAsync(userId, watchlistItemId, contentType);
        }

        //get user by id
        public async Task<UserDTODetails> GetUserById(Guid id)
        {
            return await userRepository.GetUserById(id);
        }
    }
}
