using AutoMapper;
using KeyManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace KeyManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("KeyMgmtDb")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Automapper Config
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new KeyMapProfile());
            });
            
            IMapper mapper = mappingConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);

            // HTTPClient Registration
            builder.Services.AddHttpClient("Api", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
            });

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapRazorPages();

            app.Run();
        }
    }
}
