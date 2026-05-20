using House_renting_system_Project.Data.Data;
using House_renting_system_Project.Data.Data.Entities;
using House_renting_system_Project.Extensions;
using House_renting_system_Project.Middlewares;
using House_Renting_System_Project.Services.Contracts;
using House_Renting_System_Project.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder
    .Services
    .AddDbContext<HouseRentingDbContext>(
        options => options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()))
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<HouseRentingDbContext>()
    .AddDefaultTokenProviders();

builder
    .Services
    .ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Home/Error?statusCode=401";
    });

builder
    .Services
    .AddSingleton<IStatisticsService, StatisticsService>()
    .AddScoped<IHouseService, HouseService>()
    .AddScoped<IAuthService, AuthService>()
    .AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var data = scope
        .ServiceProvider
        .GetRequiredService<HouseRentingDbContext>();

    // Only apply pending migrations — skips any that are already applied.
    // This prevents the "object already exists" crash when the DB was
    // previously seeded but the __EFMigrationsHistory table is missing
    // or incomplete.
    var pendingMigrations = (await data.Database.GetPendingMigrationsAsync()).ToList();
    if (pendingMigrations.Count > 0)
    {
        try
        {
            await data.Database.MigrateAsync();
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
            when (ex.Number == 2714) // "There is already an object named '...'" 
        {
            // Tables already exist but __EFMigrationsHistory is missing the record.
            // Insert the migration history row manually so EF Core won't try again.
            foreach (var migration in pendingMigrations)
            {
                await data.Database.ExecuteSqlRawAsync(
                    $"IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = '{migration}') " +
                    $"INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) " +
                    $"VALUES ('{migration}', '8.0.0')");
            }
        }
    }

    await data.Database.ExecuteSqlRawAsync(
        "IF OBJECT_ID(N'[Houses]', N'U') IS NOT NULL AND COL_LENGTH(N'[Houses]', N'IsDeleted') IS NULL ALTER TABLE [Houses] ADD [IsDeleted] bit NOT NULL CONSTRAINT [DF_Houses_IsDeleted] DEFAULT CAST(0 AS bit);");

    await app.SeedRoles();
    await app.SeedHouses();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/ServerError");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseStatistics();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
