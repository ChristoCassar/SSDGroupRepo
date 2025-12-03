
// I, Nicholas Cassar, student number 000902104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lab1_SecureSoftware_NicholasCassar.Data;
using Lab1_SecureSoftware_NicholasCassar.Models;
using Azure.Identity;

namespace Lab1_SecureSoftware_NicholasCassar;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        //Here weve configured application and identity user and role. 
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Lockout.MaxFailedAccessAttempts = 30;  // Maximum login attempts before lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // 5 minute lockout duration

        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true; 
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;  
            options.Cookie.SameSite = SameSiteMode.Strict;  
            options.SlidingExpiration = true;  
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Session timeout
        });

        builder.Services.AddControllersWithViews();

        var kvUri = new Uri(builder.Configuration.GetSection("KVURI").Value);
        var azCred = new DefaultAzureCredential();

        builder.Configuration.AddAzureKeyVault(kvUri, azCred);

        DbInitializer.password = builder.Configuration.GetSection("userPassword").Value;

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            DbInitializer.SeedUsersAndRoles(scope.ServiceProvider).Wait();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        //TODO - Add a header src with a src link for secrets (Should not need a wild card this will trigger a new security risk on owasp)
        app.Use(async (context, next) =>
        {
            var ctx = context!;
            if(ctx == null)
            {
                Console.WriteLine("CTX IS NULL");
            }

            ctx.Response.Headers["X-XSS-Protection"] = "0";
            ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
            ctx.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
            ctx.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";


            ctx.Response.Headers["Content-Security-Policy"] =
                "default-src 'self'; " +
                "script-src 'self';" +
                "object-src 'none'; " +
                "base-uri 'self'; " +
                "frame-ancestors 'none'; ";
            await next();
        });

        app.UseCookiePolicy(new CookiePolicyOptions
        {
            HttpOnly =
        Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });
 
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
