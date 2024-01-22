using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concretes;

public class ChoiceManager: BaseService<Choice>, IChoiceManager
{
    public ChoiceManager(ApplicationDbContext context) : base(context)
    {
    }
}