using IndWalks.API.Data;
using IndWalks.API.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IndWalks.API.Repository.Walks
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IndWalksDBContext _indWalksDBContext;

        public WalksRepository(IndWalksDBContext indWalksDBContext)
        {
            _indWalksDBContext = indWalksDBContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _indWalksDBContext.Walks.AddAsync(walk);
            await _indWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<string?> Delete(Guid id)
        {
            var existwalk = await _indWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existwalk == null)
            {
                return null;
            }
            _indWalksDBContext.Walks.Remove(existwalk);
            await _indWalksDBContext.SaveChangesAsync();
            return "Region Deleted Successfully!";
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true)
        {

            var walkresult = _indWalksDBContext.Walks.Include("Difficulty").Include("region").AsQueryable();

            //Filtering 
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkresult = walkresult.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Shorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkresult = (bool)isAscending ? walkresult.OrderBy(x => x.Name) : walkresult.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walkresult = (bool)isAscending ? walkresult.OrderBy(x => x.LengthInKm) : walkresult.OrderByDescending(x => x.LengthInKm);
                }
            }
            


            return await walkresult.ToListAsync();

            //var walkresult = await _indWalksDBContext.Walks.Include("Difficulty").Include("region").ToListAsync();
            //return walkresult;
        }

        public async Task<Walk?> GetById(Guid id)
        {
            return await _indWalksDBContext.Walks.Include("Difficulty").Include("region").FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateWalk(Guid id, Walk walk)
        {
            var exist = await _indWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                return null;
            }
            exist.Name = walk.Name;
            exist.Description = walk.Description;
            exist.WalkImageUrl = walk.WalkImageUrl;
            exist.DifficultyId = walk.DifficultyId;
            exist.RegionId = walk.RegionId;

            await _indWalksDBContext.SaveChangesAsync();
            return exist;
        }
    }
}
