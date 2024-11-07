using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Gamma2024.Server.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Charger les fichiers de configuration selon l'environnement
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ClientInscriptionService>();
builder.Services.AddScoped<ClientModificationService>();
builder.Services.AddScoped<EncanService>();
builder.Services.AddScoped<VendeurService>();
builder.Services.AddScoped<AdministrateurService>();
builder.Services.AddScoped<LotService>();

builder.Services.Configure<EmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration"));

builder.Services.AddTransient<IEmailSender, EmailService>();

// Configuration CORS pour différents environnements
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("Development", builder =>
        {
            builder
                .SetIsOriginAllowed(_ => true)
                .WithOrigins("https://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    }
    else
    {
        options.AddPolicy("Production", builder =>
        {
            builder
                .WithOrigins("https://sqlinfocg.cegepgranby.qc.ca")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    }
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<WebSocketHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development");
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseCors("Production");
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
//    RequestPath = ""
//});

app.MapFallbackToFile("index.html");

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets(webSocketOptions);
app.UseMiddleware<WebSocketMiddleware>();

app.Run();
