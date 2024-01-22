using BusinessLayer.Concretes;
using DataAccessLayer;
using DataAccessLayer.Concretes;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using EntityLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IChoiceManager, ChoiceManager>();
builder.Services.AddScoped<IExamManager, ExamManager>();
builder.Services.AddScoped<IQuestionManager, QuestionManager>();
builder.Services.AddScoped<IQuestionCategoryManager, QuestionCategoryManager>();
builder.Services.AddScoped<IExamAnswerManager, ExamAnswerManager>();
builder.Services.AddScoped<IExamAssignmentManager, ExamAssignmentManager>();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        SeedData.Initialize(services, userManager, roleManager).Wait();

        var logger = services.GetRequiredService<ILogger<Program>>();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();