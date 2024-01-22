using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;

namespace DataAccessLayer.Concretes;

public class CategoryManager: BaseService<Category>, ICategoryManager
{
    public CategoryManager(ApplicationDbContext context) : base(context)
    {
    }
}