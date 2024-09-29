using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TelkomLearn.Data;
using TelkomLearn.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure InMemory Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Register Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Make sure to add this
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData(services);
}

await app.RunAsync();

async Task SeedData(IServiceProvider services)
{
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Seed two users with real names
    var users = new List<(string Email, string FirstName, string LastName, int StreakDays, int MaxStreakDays)>
    {
        ("Siya.Mambila@example.com", "Siya", "Mambila", 15, 30), // John with a current streak of 15 days
        ("Thabo.Yenge@example.com", "Thabo", "Yenge", 25, 50) // Jane with a current streak of 25 days
    };

    foreach (var (email, firstName, lastName, streakDays, maxStreakDays) in users)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = firstName,
            LastName = lastName
        };

        // Create the user if it doesn't already exist
        var result = await userManager.CreateAsync(user, "Password123!");

        if (result.Succeeded)
        {
            var userStreak = new UserStreak
            {
                UserId = user.Id,
                StreakDays = streakDays,
                MaxStreakDays = maxStreakDays
            };

            context.UserStreaks.Add(userStreak);
            await context.SaveChangesAsync();
        }
    }
}
