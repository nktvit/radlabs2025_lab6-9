using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductModel;
using ProductModel.GRN;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebAPI2025.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider serviceProvider)
        {
            // Create roles if they don't exist
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            
            try
            {
                // Seed roles
                string[] roleNames = { "Store Manager" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Ensure roles are saved to the database before adding users to roles
                var identityDbContext = serviceProvider.GetRequiredService<ApplicationDBContext>();
                await identityDbContext.SaveChangesAsync();

                // Seed users
                var storeManager = new IdentityUser
                {
                    UserName = "Durkin.Rosaleen@itsligo.ie",
                    Email = "Durkin.Rosaleen@itsligo.ie",
                    EmailConfirmed = true
                };

                var user = await userManager.FindByEmailAsync(storeManager.Email);
                if (user == null)
                {
                    var result = await userManager.CreateAsync(storeManager, "P@ssword!");
                    if (result.Succeeded)
                    {
                        // Ensure user is saved to the database before adding to role
                        await identityDbContext.SaveChangesAsync();
                        
                        // Check if role exists before adding user to role
                        if (await roleManager.RoleExistsAsync("Store Manager"))
                        {
                            await userManager.AddToRoleAsync(storeManager, "Store Manager");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding identity data: {ex.Message}");
                throw;
            }

            // Seed GRN data
            var dbContext = serviceProvider.GetRequiredService<ProductDBContext>();
            
            // Check if GRN data already exists
            if (!dbContext.GRNs.Any())
            {
                // Create GRN data
                var grn1 = new GRN
                {
                    OrderDate = new DateTime(2022, 2, 23),
                    DeliveryDate = new DateTime(2022, 3, 20),
                    StockUpdated = false
                };

                var grn2 = new GRN
                {
                    OrderDate = new DateTime(2022, 2, 24),
                    DeliveryDate = null,
                    StockUpdated = false
                };

                dbContext.GRNs.Add(grn1);
                dbContext.GRNs.Add(grn2);
                await dbContext.SaveChangesAsync();

                // Create GRN Lines
                var grnLine1 = new GRNLine
                {
                    GrnID = 1,
                    StockID = 1,
                    QtyDelivered = 20
                };

                var grnLine2 = new GRNLine
                {
                    GrnID = 1,
                    StockID = 2,
                    QtyDelivered = 40
                };

                var grnLine3 = new GRNLine
                {
                    GrnID = 1,
                    StockID = 3,
                    QtyDelivered = 70
                };

                var grnLine4 = new GRNLine
                {
                    GrnID = 2,
                    StockID = 9,
                    QtyDelivered = 20
                };

                dbContext.GRNLines.Add(grnLine1);
                dbContext.GRNLines.Add(grnLine2);
                dbContext.GRNLines.Add(grnLine3);
                dbContext.GRNLines.Add(grnLine4);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
