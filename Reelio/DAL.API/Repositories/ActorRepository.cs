using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private static readonly HttpClient _client = new HttpClient(); 
        private readonly string _apiBaseUrl;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ActorRepository(IConfiguration configuration)
        {
            _apiBaseUrl = configuration["NMDB_URL"];
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        private string BuildQueryString(int pageNumber, int pageSize, string? searchQuery)
        {
            var queryString = $"?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(searchQuery))
                queryString += $"&searchQuery={Uri.EscapeDataString(searchQuery)}";

            return queryString;
        }

        public async Task<PaginatedList<ActorDTO>> GetActors(int pageNumber, int pageSize, string? searchQuery)
        {
            var queryString = BuildQueryString(pageNumber, pageSize, searchQuery);
            Uri url = new Uri(_apiBaseUrl + "/Actor" + queryString);

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch actors. Status Code: {response.StatusCode}");

                var paginatedList = JsonSerializer.Deserialize<PaginatedList<Actor>>(content, _jsonSerializerOptions);

                if (paginatedList == null)
                    throw new InvalidOperationException("Failed to deserialize paginated list.");

                paginatedList.Items ??= new List<Actor>();

                var actorDTOs = paginatedList.Items.Select(actorEntity => new ActorDTO
                {
                    Id = actorEntity.Id,
                    Name = actorEntity.Name,
                    ImageUrl = actorEntity.ImageUrl
                }).ToList();

                return new PaginatedList<ActorDTO>(actorDTOs, paginatedList.TotalCount, paginatedList.PageNumber, paginatedList.PageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching actors: {ex.Message}", ex);
            }
        }

        public async Task<ActorDTODetails> GetActorById(int id)
        {
            Uri url = new Uri(_apiBaseUrl + "/actor/" + id);

            try
            {
                var response = await _client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch actor with ID {id}. Status Code: {response.StatusCode}");

                var actorEntity = JsonSerializer.Deserialize<Actor>(content, _jsonSerializerOptions);

                if (actorEntity == null)
                    throw new InvalidOperationException("Failed to deserialize actor.");

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
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching actor by ID {id}: {ex.Message}", ex);
            }
        }
    }
}
