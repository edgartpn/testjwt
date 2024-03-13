using APINetCore.Models;
using APINetCore.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


// Configura la autenticación JWT
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var jwtSecretKey = builder.Configuration.GetValue<string>("JwtSettings:SecretKey");
var jwtAudienceToken = builder.Configuration.GetValue<string>("JwtSettings:AudienceToken");
var jwtIssuerToken = builder.Configuration.GetValue<string>("JwtSettings:IssuerToken");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuerToken, // Cambia esto al emisor deseado
            ValidAudience = jwtAudienceToken, // Cambia esto al público deseado
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)) // Cambia esto a tu clave secreta

        };
    });
//bearen


var conn = builder.Configuration.GetConnectionString("DefaultConnection");
// Configurar el DbContext
builder.Services.AddDbContext<MembershipContext>(options =>
    options.UseSqlServer(conn));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//control de autorize
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
   
    // Agrega un esquema de seguridad Swagger para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Añade un requerimiento de seguridad para JWT
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
            Array.Empty<string>()
        }
    });
});

//
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseTokenValidationMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
