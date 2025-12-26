using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NamPractice.API.Models.Domain;

namespace Practice.API.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetIdAsync(Guid id);
    Task<Region> CreateAsync(Region region);
    Task<Region?> DeleteAsync(Guid id);
    Task<Region?> UpdateAsync(Guid id, Region region);
}