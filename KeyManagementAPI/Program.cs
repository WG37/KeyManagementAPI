using KeyManagementAPI.Data;
using KeyManagementAPI.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KeyManagementAPI.Services.AsymmetricServices.KeyServices;
using KeyManagementAPI.Services.AsymmetricServices.CipherServices;
using KeyManagementAPI.Services.SymmetricServices.KeyServices;
using KeyManagementAPI.Services.SymmetricServices.CipherServices;
using KeyManagementAPI.Services.PredictServices;


namespace KeyManagementAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("KeyMgmtDb")));


            // Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(o =>
            {
                o.Cookie.Name = ".Key.API";
                o.LoginPath = "/Identity/Account/Login";
                o.LogoutPath = "/IdentityAccount/Logout";
                o.AccessDeniedPath = "/IdentityAccount/AccessDenied";
                o.ExpireTimeSpan = TimeSpan.FromHours(8);
                o.SlidingExpiration = true;
            });

            // JWT auth
            var jwtCfg = builder.Configuration.GetSection("jwt");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtCfg["Key"]!));

            // let ASP.net know we are registering auth system -- using our jwt authentication
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;   // process: read bearer header => validate token => set HttpContext.User // REVIEW //
                o.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;      // process: if request is unauthenticated => respond with 401 Unauthorized // REVIEW //
            })
             .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, bearerOptions =>
             {
                 bearerOptions.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = jwtCfg["Issuer"],

                     ValidateAudience = true,
                     ValidAudience = jwtCfg["Audience"],

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingKey,

                     ClockSkew = TimeSpan.Zero
                 };
             });

            // Service Registration
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IKeyService, KeyService>();
            builder.Services.AddScoped<ICipherService, CipherService>();

            builder.Services.AddScoped<IAsymKeyService, AsymKeyService>();
            builder.Services.AddScoped<IAsymCipherService, AsymCipherService>();

            builder.Services.AddSingleton<IBruteEstimationService, BruteEstimationService>();
            builder.Services.AddScoped<IEmailSender, DummyEmailSender>();

            builder.Services.AddRazorPages();

            var app = builder.Build();
            
            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();
            // seed call
           // await SeedAsync(app);

            app.Run();

            // admin seeding
            /*
            static async Task SeedAsync(IHost host)
            {
                using var scope = host.Services.CreateScope();

                var roleMgmt = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userMgmt = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                const string adminRole = "Admin";
                const string adminEmail = "Admin@keymail.com";
                const string adminPass = "Admin123!";

                // roll exists conditional 
                if (!await roleMgmt.RoleExistsAsync(adminRole))
                    await roleMgmt.CreateAsync(new IdentityRole(adminRole));

                // user exists conditional
                var admin = await userMgmt.FindByEmailAsync(adminEmail);
                if (admin == null)
                {
                    admin = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userMgmt.CreateAsync(admin, adminPass);
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(",", result.Errors.Select(e => e.Description));
                        throw new Exception($"Admin seeding failed: {errors}");
                    }
                    await userMgmt.AddToRoleAsync(admin, adminRole); 
                }
            }  */
        }
    }
}
