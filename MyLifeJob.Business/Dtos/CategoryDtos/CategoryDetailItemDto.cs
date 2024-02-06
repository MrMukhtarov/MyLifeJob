﻿using Microsoft.AspNetCore.Http;

namespace MyLifeJob.Business.Dtos.CategoryDtos;

public record CategoryDetailItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public bool IsDeleted { get; set; }
}
