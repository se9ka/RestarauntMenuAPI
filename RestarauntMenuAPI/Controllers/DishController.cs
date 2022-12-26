using Microsoft.AspNetCore.Mvc;
using RestarauntMenu.Bll;
using RestarauntMenu.Dal;
using System.ComponentModel.DataAnnotations;

namespace RestarauntMenuAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddDishAsync(DishEntity dish)
    {
        var dishAdded = await _dishService.AddAsync(dish);

        return CreatedAtRoute("GetDish",
            new
            {
                dishAdded.Id,
                dishAdded.Price,
                dishAdded.Name,
                dishAdded.Cuisine,
                dishAdded.Ingredients,
                dishAdded.DishType
            }, dishAdded);
    }

    [HttpGet]
    public async Task<IActionResult> GetDishesAsync(bool orderByVotes)
    {
        var dishesFromRepo = await _dishService.GetAsync(orderByVotes);

        return Ok(dishesFromRepo);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetDishesWithoutIngridientsAsync([FromBody] List<string> ingridients)
    {
        var dishesFromRepo = await _dishService.GetDishesWithoutIngridientsAsync(ingridients);

        return Ok(dishesFromRepo);
    }

    [HttpGet("{id}", Name = "GetDish")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDishAsync([Required] int id)
    {
        var dishFromRepo = await _dishService.FindAsync(new DishEntity { Id = id });

        if (dishFromRepo == null) return NotFound();

        return Ok(dishFromRepo);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDishAsync([Required] int dishId, DishEntity dishForUpdate)
    {
        if (!await _dishService.IsExistAsync(new DishEntity { Id = dishId })) return NotFound();

        var updatedDish = dishForUpdate;
        updatedDish.Id = dishId;

        await _dishService.UpdateAsync(updatedDish);

        return Ok(updatedDish);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDishAsync([Required] int dishId)
    {
        if (!await _dishService.IsExistAsync(new DishEntity { Id = dishId })) return NotFound();

        var dishForDelete = new DishEntity { Id = dishId };
        await _dishService.DeleteAsync(dishForDelete);

        return Ok(dishForDelete);
    }
}