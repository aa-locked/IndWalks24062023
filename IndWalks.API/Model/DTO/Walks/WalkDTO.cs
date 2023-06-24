﻿namespace IndWalks.API.Model.DTO.Walks
{
    public class WalkDTO
    {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double LengthInKm { get; set; }
            public string? WalkImageUrl { get; set; }

            public Guid DifficultyId { get; set; }
            public Guid RegionId { get; set; }

            public DiffcultyDTO Difficulty { get; set; }
            public RegionDTO region { get; set; }
        
    }
}
