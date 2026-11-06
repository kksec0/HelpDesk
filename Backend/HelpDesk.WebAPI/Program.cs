using HelpDesk.WebAPI.Data;
using HelpDesk.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HelpDeskDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        // E-posta benzersiz olmalı.
        options.User.RequireUniqueEmail = true;

        // Parola kuralları.
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<HelpDeskDBContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//CORS ayarları
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


//rolleri oluşturma  ve admin hesabını oluşturma
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var roleManager = serviceProvider
        .GetRequiredService<RoleManager<IdentityRole<int>>>();

    var userManager = serviceProvider
        .GetRequiredService<UserManager<User>>();

    // Rolleri oluştur
    string[] roles = { "Admin", "Technician", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole<int>(role));
        }
    }

    // Admin bilgilerini User Secrets üzerinden oku
    var adminSection = builder.Configuration
        .GetSection("AdminAccount");

    var adminEmail = adminSection["Email"];
    var adminPassword = adminSection["Password"];
    var adminFullName = adminSection["FullName"];

    if (string.IsNullOrWhiteSpace(adminEmail) ||
        string.IsNullOrWhiteSpace(adminPassword) ||
        string.IsNullOrWhiteSpace(adminFullName))
    {
        throw new Exception("Admin hesap bilgileri eksik!");
    }

    // Admin hesabını kontrol et
    var admin = await userManager.FindByEmailAsync(adminEmail);

    if (admin == null)
    {
        admin = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            AdSoyad = adminFullName,
            EmailConfirmed = true,
            AktifMi = true,
            IlkGirisMi = true
        };

        var result = await userManager.CreateAsync(
            admin, adminPassword);

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    // Admin rolünü ata
    if (!await userManager.IsInRoleAsync(admin, "Admin"))
    {
        await userManager.AddToRoleAsync(admin, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
