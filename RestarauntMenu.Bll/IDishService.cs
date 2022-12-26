using RestarauntMenu.Dal;

namespace RestarauntMenu.Bll;

public interface IDishService
{
    Task<DishEntity> AddAsync(DishEntity dish);

    Task<DishEntity> UpdateAsync(DishEntity dish);

    Task<DishEntity> DeleteAsync(DishEntity dish);

    Task<IEnumerable<DishEntity>> GetAsync(bool dishByVotesByDescending);

    Task<DishEntity?> FindAsync(DishEntity dishEntity);

    Task<bool> IsExistAsync(DishEntity dishEntity);

    Task<IEnumerable<DishEntity>> GetDishesWithoutIngridientsAsync(List<string> ingredients);
}
