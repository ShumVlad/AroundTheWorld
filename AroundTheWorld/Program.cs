using AroundTheWorld;
using AroundTheWorld_Backend;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(
    typeof(AroundTheWorld_Backend.MappingProfile),
    typeof(PresentationMappingProfile)
);

builder.Services.AddDbContext<AroundTheWorldDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AroundTheWorldDbContext>()
                .AddDefaultTokenProviders();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IRepository<Group>, Repository<Group>>();
builder.Services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();
builder.Services.AddScoped<IRepository<Location>, Repository<Location>>();
builder.Services.AddScoped<IRepository<Media>, Repository<Media>>();
builder.Services.AddScoped<IRepository<AroundTheWorld_Persistence.Models.Route>, Repository<AroundTheWorld_Persistence.Models.Route>>();
builder.Services.AddScoped<IRepository<Sensor>, Repository<Sensor>>();
builder.Services.AddScoped<IRepository<Review>, Repository<Review>>();
builder.Services.AddScoped<IRepository<UserPosition>, Repository<UserPosition>>();
builder.Services.AddScoped<IRepository<RentItem>, Repository<RentItem>>();
builder.Services.AddScoped<IRepository<LocationRoute>, Repository<LocationRoute>>();
builder.Services.AddScoped<IRepository<UserGroup>, Repository<UserGroup>>();
builder.Services.AddScoped<IUserGroupExtraRepository, UserGroupExtraRepository>();
builder.Services.AddScoped<ILocationRouteExtraRepository, LocationRouteExtraRepository>();
builder.Services.AddScoped<IRepository<Company>, Repository<Company>>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILocationRouteService, LocationRouteService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IUserPositionService, UserPositionService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<IRentItemService, RentItemService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AroundTheWorldDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
