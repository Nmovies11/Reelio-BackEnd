using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using Common.DTO;
using DAL.API.Entities;
using Microsoft.Extensions.Configuration;

namespace DAL.API.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiBaseUrl;

        public ActorRepository(IConfiguration configuration)
        {

            _apiBaseUrl = configuration["NMDB_URL"];
        }

        public async Task<PaginatedList<ActorDTO>> GetActors(int pageNumber, int pageSize, string? searchQuery)
        {
            var queryString = $"?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                queryString += $"&searchQuery={Uri.EscapeDataString(searchQuery)}";
            }



            Uri url = new Uri(_apiBaseUrl + "/Actor" + queryString);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch actors. Status Code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + content);

            var paginatedList = JsonSerializer.Deserialize<PaginatedList<Actor>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (paginatedList == null)
            {
                throw new InvalidOperationException("Failed to deserialize paginated list.");
            }

            paginatedList.Items ??= new List<Actor>();

            var actorDTOs = paginatedList.Items.Select(actorEntity => new ActorDTO
            {
                Id = actorEntity.Id,
                Name = actorEntity.Name,
                ImageUrl = actorEntity.ImageUrl
            }).ToList();

            // Return the paginated list of ActorDTOs
            return new PaginatedList<ActorDTO>(actorDTOs, paginatedList.TotalCount, paginatedList.PageNumber, paginatedList.PageSize);
        }

        public async Task<ActorDTODetails> GetActorById(int id)
        {
            Uri url = new Uri(_apiBaseUrl + "/actor/" + id);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch actor. Status Code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + content);

            var actorEntity = JsonSerializer.Deserialize<Actor>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (actorEntity == null)
            {
                throw new InvalidOperationException("Failed to deserialize actor.");
            }

            var actorDTO = new ActorDTODetails
            {
                Id = actorEntity.Id,
                Name = actorEntity.Name,
                ImageUrl = actorEntity.ImageUrl,
                Bio = actorEntity.Bio,
                BirthDate = actorEntity.BirthDate,
            };
            return actorDTO;
        }
    }
}
