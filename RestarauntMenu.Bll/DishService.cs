using Microsoft.EntityFrameworkCore;
using RestarauntMenu.Dal;

namespace RestarauntMenu.Bll;

public sealed class DishService : IDishService
{
    protected readonly RestarauntContext _context;

    public DishService(RestarauntContext context)
    {
        _context = context;
    }

    public async Task<DishEntity> AddAsync(DishEntity dish)
    {
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();

        return dish;
    }

    public async Task<DishEntity> UpdateAsync(DishEntity dish)
    {
        _context.Dishes.Update(dish);
        await _context.SaveChangesAsync();

        return dish;
    }

    public async Task<DishEntity> DeleteAsync(DishEntity dish)
    {
        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();

        return dish;
    }

    public async Task<IEnumerable<DishEntity>> GetAsync(bool dishByVotesByDescending)
    {
        var dishes = _context.Dishes.AsQueryable();

        return dishByVotesByDescending ? await dishes.OrderByDescending(e => e.Votes).ToListAsync() : await dishes.ToListAsync();
    }

    public async Task<DishEntity?> FindAsync(DishEntity dishEntity)
    {
        return await _context.Dishes
             .Where(dish => string.IsNullOrEmpty(dishEntity.Name) || dish.Name.ToLower().Contains(dishEntity.Name.ToLower()))
             .Where(dish => string.IsNullOrEmpty(dishEntity.Ingredients) || dish.Ingredients.ToLower().Contains(dishEntity.Ingredients.ToLower()))
             .Where(dish => string.IsNullOrEmpty(dishEntity.Cuisine) || dish.Cuisine.ToLower().Contains(dishEntity.Cuisine.ToLower()))
             .Where(dish => string.IsNullOrEmpty(dishEntity.DishType) || dish.DishType.ToLower().Contains(dishEntity.DishType.ToLower()))
             .Where(dish => !dishEntity.Price.HasValue || dish.Price == dishEntity.Price)
             .Where(dish => dish.Id == dishEntity.Id)
             .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistAsync(DishEntity dishEntity)
    {
        return await _context.Dishes
             .Where(dish => string.IsNullOrEmpty(dishEntity.Name) || dish.Name.ToLower().Contains(dishEntity.Name.ToLower()))
             .Where(dish => string.IsNullOrEmpty(dishEntity.Ingredients) || dish.Ingredients.ToLower().Contains(dishEntity.Ingredients.ToLower()))
             .Where(dish => string.IsNullOrEmpty(dishEntity.Cuisine) || dish.Cuisine.ToLower().Contains(dishEntity.Cuisine.ToLower()))
             .Where(dish => string.IsNullOrEmpty(dishEntity.DishType) || dish.DishType.ToLower().Contains(dishEntity.DishType.ToLower()))
             .Where(dish => !dishEntity.Price.HasValue || dish.Price == dishEntity.Price)
             .Where(dish => dish.Id == dishEntity.Id)
             .AnyAsync();
    }

    public async Task<IEnumerable<DishEntity>> GetDishesWithoutIngridientsAsync(List<string> ingredients)
    {
        var dishes = await _context.Dishes.ToListAsync();

        return dishes.Where(dish =>
            !dish.Ingredients.Split(' ', ',').ToList().Any(ingredients.Contains)
        ).ToList();
    }
}
