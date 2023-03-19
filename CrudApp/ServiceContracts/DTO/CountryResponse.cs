﻿using Entities;
using System;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as the return type for most of CountryService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                Id = country.Id,
                Name = country.Name,
            };
        }
    }
}