using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurações do Azure AD
var azureAdConfig = builder.Configuration.GetSection("AzureAd");
var instance = azureAdConfig["Instance"];
var tenantId = azureAdConfig["TenantId"];
var clientId = azureAdConfig["ClientId"];
var validIssuer = azureAdConfig["ValidIssuer"].Replace("{TenantId}", tenantId);

// Configuração do JWT para validar tokens de apps clientes
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Autoridade do Azure AD (issuer público)
        options.Authority = $"{instance}{tenantId}/v2.0/";
        options.Audience = clientId;
        options.RequireHttpsMetadata = true;
        // Remove the duplicate 'ValidateIssuerSigningKey' assignment.
        // Only keep one assignment for 'ValidateIssuerSigningKey' in TokenValidationParameters.

        // **Gerenciamento automático das chaves públicas**
        options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            $"{options.Authority}/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever()
        );

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = validIssuer,

            ValidateAudience = true,
            ValidAudience = $"api://{clientId}", // O ClientId da SUA API

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5),

            // Não usamos chave simétrica (client_secret) porque quem valida a assinatura é o Azure AD
            ValidateIssuerSigningKey = true, // Valida a assinatura do token

            RequireSignedTokens = true, // Garante que só tokens assinados são aceitos

            RoleClaimType = "roles"
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"[ERRO JWT] Token inválido: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var claims = context.Principal.Claims;
                Console.WriteLine($"[JWT OK] Token válido para app: {context.Principal.Identity.Name}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
    c.RoutePrefix = string.Empty; // Faz com que o Swagger seja exibido na raiz "/"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
