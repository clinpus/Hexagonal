using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Text;
using System.Text.Json;

// 1. Définir la politique CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(
                              "http://localhost:4200",        // ⬅️ Votre application Angular
                              "https://localhost:4200",        // ⬅️ Votre application Angular
                              "http://localhost:5029",        // ⬅️ L'URL de votre API/Swagger
                              "https://localhost:5029",        // ⬅️ L'URL de votre API/Swagger
                              "https://votre-app-angular.com") // ⬅️ Votre production
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false, // À configurer pour la production
            ValidateAudience = false // À configurer pour la production
        };
    });

// Add services to the container.

builder.Services.AddControllers();

// Récupération de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("HexgonalConnection");

// Enregistrement de votre DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserHandler, UserHandler>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IInvoiceHandler, InvoiceHandler>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerHandler, CustomerHandler>();

// Ajout des services pour le générateur Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // C'est ici que vous pouvez personnaliser le titre et la version de votre doc.
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Votre API de Facturation",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Entrez le token JWT dans le champ ci-dessous. Exemple : Bearer [mon_token_ici]",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// 0
var app = builder.Build();

// 1  Configure the HTTP request pipeline.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// 2 
app.UseHttpsRedirection();

// 3. Définir le routing (MapControllers doit être précédé de UseRouting implicitement ou explicitement)
app.UseRouting();

// 4. **CORS** : Doit être appliqué ici (après UseRouting) pour que la politique s'applique 
//    aux requêtes entrantes, y compris celles de Swagger UI.
app.UseCors(MyAllowSpecificOrigins); // ⬅️ AJOUTEZ CETTE LIGNE

// 5 
app.UseAuthentication();
app.UseAuthorization();

// 6  Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Ajout du middleware Swagger et SwaggerUI
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.Run();
