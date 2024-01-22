using EntityLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Abstract;

public interface IService<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
}