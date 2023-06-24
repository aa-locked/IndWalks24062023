using IndWalks.API.Model.Domain;
using IndWalks.API.Model.DTO.Walks;

namespace IndWalks.API.Repository.Walks
{
    public interface IWalksRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true);
        Task<Walk?> GetById(Guid id);
        Task<Walk?> UpdateWalk(Guid id, Walk walk);
        Task<string?> Delete(Guid id);
    }
}
