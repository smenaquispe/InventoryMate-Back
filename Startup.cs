using Microsoft.EntityFrameworkCore;
using InventoryMate.Data;
using InventoryMate.Services;
using InventoryMate.Repositories;
using InventoryMate.Utilities;

public class Startup {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection _services) {
        _services.AddControllers();
        _services.AddEndpointsApiExplorer();
        _services.AddSwaggerGen();

        var configurationString = _configuration.GetConnectionString("DefaultConnection");
        _services.AddDbContext<AppDbContext> (options => {
            options.UseMySql(configurationString, ServerVersion.AutoDetect(configurationString));
        });


        string key = _configuration["Jwt:Key"]!;
        JwtHandler.Configure(key, _services);

        _services.AddScoped<IProductRepository, ProductRepository>();
        _services.AddScoped<IUserRepository, UserRepository>();
        _services.AddScoped<ITransactionRepository, TransactionRepository>();
        _services.AddScoped<INotificationRepository, NotificationRepository>();
        _services.AddScoped<IStoreRepository, StoreRepository>();
        _services.AddScoped<ISaleRepository, SaleRepository>();

        _services.AddScoped<IProductService, ProductService>();
        _services.AddScoped<IUserService, UserService>();
        _services.AddScoped<ITransactionService, TransactionService>();
        _services.AddScoped<ISaleService, SaleService>();
        _services.AddScoped<INotificationService, NotificationService>();
        _services.AddScoped<IStoreService, StoreService>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}