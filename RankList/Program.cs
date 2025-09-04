using FirebaseAdmin;
using RankList.Services;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using RankList.Models;

using RankList.Repositories; // Adicione este using

var builder = WebApplication.CreateBuilder(args);

// Inicialize o Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("serviceAccountKey.json")
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// REPOSITÓRIO
builder.Services.AddScoped<IListaPessoalRepository, ListaPessoalRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Receba o token do header Authorization
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                context.Token = token;
                return Task.CompletedTask;
            },
            OnTokenValidated = async context =>
            {
                var token = context.SecurityToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
                if (token != null)
                {
                    try
                    {
                        var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token.RawData);
                        // Você pode adicionar claims customizadas aqui
                    }
                    catch
                    {
                        context.Fail("Token inválido do Firebase.");
                    }
                }
            }
        };
        // Não configure Authority/Issuer, pois o Firebase valida o token
    });

var app = builder.Build();

// Configurar o HTTP request
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Remova o modificador 'public' da função local e torne-a estática conforme sugerido pelo IDE0062.
// Se não for utilizada, considere removê-la para evitar CS8321.
// Caso queira mantê-la para uso futuro, deixe como função local estática sem o modificador 'public'.

static async Task<bool> ValidateFirebaseToken(string idToken)
{
    try
    {
        var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
        // decodedToken.Uid contém o ID do usuário autenticado
        return true;
    }
    catch
    {
        return false;
    }
}
