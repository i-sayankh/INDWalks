﻿namespace INDWalks.API.Models.DTOs
{
    public class AddRegionRequestDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}