using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductModel;
using ProductModel.GRN;
using ProductWebAPI2025.Data;
using System.Text;
using Tracker.WebAPIClient;

namespace ProductWebAPI2025
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ActivityAPIClient.Track("S00242994", "Mykyta Vitkovsky", activityName: "RAD30223 Week 8 Lab 1",
                Task: "Running Web API");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ProductDBContext>();
            
            // Add Identity
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlite("Data Source=Week8ProductCoreDB-2025-S00242994.db"));
            
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            // Add JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtAudience"] ?? "YourAudience",
                    ValidIssuer = builder.Configuration["JwtIssuer"] ?? "YourIssuer",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration["JwtKey"] ?? "YourSecretKeyHere12345678901234567890"))
                };
            });
            
            builder.Services.AddTransient<IProduct<Product>, ProductRepository>();
            builder.Services.AddTransient<IGRN<ProductModel.GRN.GRN>, ProductModel.GRN.GRNRepository>();
            
            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorApp",
                    builder => builder
                        .WithOrigins("https://localhost:7125") // Blazor app URL
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors("AllowBlazorApp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Apply migrations and seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Apply migrations for ApplicationDBContext
                    var applicationDbContext = services.GetRequiredService<ApplicationDBContext>();
                    applicationDbContext.Database.Migrate();
                    
                    // Apply migrations for ProductDBContext
                    var productDbContext = services.GetRequiredService<ProductDBContext>();
                    productDbContext.Database.Migrate();
                    
                    // Seed the database
                    await DbSeeder.SeedDefaultData(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }

            app.Run();
        }
    }
}
