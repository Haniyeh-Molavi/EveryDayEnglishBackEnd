using LanguageLearning.Application.DTOs.Categories;
using LanguageLearning.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        return Ok(await _categoryService.GetAllAsync());
    }
}
