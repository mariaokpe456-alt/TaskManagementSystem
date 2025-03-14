
////using CollabTaskManager.Data;
////using CollabTaskManager.Hubs;
////using CollabTaskManager.Models;
////using CollabTaskManager.Services.Implementations;
////using CollabTaskManager.Services.Interfaces;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.IdentityModel.Tokens;
////using System.Text;

////var builder = WebApplication.CreateBuilder(args);

//////Database Configuration
////builder.Services.AddDbContext<AppDbContext>(options =>
////    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//////// Get the connection string
//////var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//////// Configure DbContext
//////builder.Services.AddDbContext<AppDbContext>(options =>
//////    options.UseSqlServer(connectionString, sqlOptions =>
//////        sqlOptions.EnableRetryOnFailure()));

////// Identity Configuration
////builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
////    .AddEntityFrameworkStores<AppDbContext>()
////    .AddDefaultTokenProviders();


//////Dependence injection
////builder.Services.AddScoped<IAuthService, AuthService>();
////builder.Services.AddScoped<RoleManager<IdentityRole>>();
////builder.Services.AddScoped< IProjectRepository,  ProjectRepository>();
////builder.Services.AddScoped<ITaskRepository, TaskRepository>();
////builder.Services.AddScoped<ICommentRepository, CommentRepository>();
////builder.Services.AddScoped<IFileRepository, FileRepository>();
////builder.Services.AddSignalR();
////builder.Services.AddSingleton<NotificationService>();
////builder.Services.AddScoped<AnalyticsService>();
////builder.Services.AddScoped<INotificationService, NotificationService>();



////// JWT Authentication
////var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
////builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////    .AddJwtBearer(options =>
////    {
////        options.TokenValidationParameters = new TokenValidationParameters
////        {
////            ValidateIssuer = true,
////            ValidateAudience = true,
////            ValidateLifetime = true,
////            ValidateIssuerSigningKey = true,
////            ValidIssuer = builder.Configuration["Jwt:Issuer"],
////            ValidAudience = builder.Configuration["Jwt:Audience"],
////            IssuerSigningKey = new SymmetricSecurityKey(key)
////        };
////    });

////builder.Services.AddControllers();
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var app = builder.Build();

////// Seed Roles
////using (var scope = app.Services.CreateScope())
////{
////    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
////    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

////    string[] roles = { "Admin", "TeamMember" };

////    foreach (var role in roles)
////    {
////        if (!await roleManager.RoleExistsAsync(role))
////            await roleManager.CreateAsync(new IdentityRole(role));
////    }
////}

////// Configure middleware
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}



////app.UseEndpoints(endpoints =>
////{
////    endpoints.MapControllers();
////    endpoints.MapHub<NotificationHub>("/notificationHub");
////});
////app.UseStaticFiles(); // Add this line to serve files from wwwroot
////app.UseRouting();
////app.MapHub<NotificationHub>("/notifications");
////app.UseHttpsRedirection();
////app.UseAuthentication();
////app.UseAuthorization();
//////app.MapControllers();
////app.Run();

//using CollabTaskManager.Data;
//using CollabTaskManager.Hubs;
//using CollabTaskManager.Models;
//using CollabTaskManager.Services.Implementations;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// ?? Database Configuration
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ?? Identity Configuration
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();

//// ?? Dependency Injection (DI)
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<RoleManager<IdentityRole>>();
//builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
//builder.Services.AddScoped<ITaskRepository, TaskRepository>();
//builder.Services.AddScoped<ICommentRepository, CommentRepository>();
//builder.Services.AddScoped<IFileRepository, FileRepository>();
//builder.Services.AddScoped<IAnalyticsService, AnalyticsService>(); // Now AnalyticsService implements IAnalyticsService
//builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddSignalR(); // Add SignalR Support

//// ?? JWT Authentication Configuration
//var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(key)
//        };
//    });

//// ?? Register MVC, Swagger, and API Explorer
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// ?? Seed Roles (Admin, TeamMember)
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//    string[] roles = { "Admin", "TeamMember" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//            await roleManager.CreateAsync(new IdentityRole(role));
//    }
//}

//// ?? Configure Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseStaticFiles(); // Serve static files (index.html, etc.)
//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

//// ?? Register API Endpoints & SignalR Hubs
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapHub<NotificationHub>("/notificationHub"); // Register SignalR hub
//});

//app.Run();



using CollabTaskManager.Data;
using CollabTaskManager.Hubs;
using CollabTaskManager.Models;
using CollabTaskManager.Services.Implementations;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ?? Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ?? Dependency Injection (DI)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>(); // ? Fixed
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddSignalR(); // Add SignalR Support

// ?? JWT Authentication Configuration
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// ?? Register MVC, Swagger, and API Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ?? Seed Roles (Admin, TeamMember)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "TeamMember" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// ?? Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1"));
}



app.UseStaticFiles(); // Serve static files (index.html, etc.)
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ?? Register API Endpoints & SignalR Hubs
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/notificationHub");
});

app.Run();

