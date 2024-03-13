
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace APINetCore.Service

{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IConfiguration _configuration;

        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        private static bool TryRetrieveToken(HttpContext request, out string token)
        {
            token = null;
            var authzHeaders = new Microsoft.Extensions.Primitives.StringValues();
            if (!request.Request.Headers.TryGetValue("Authorization", out authzHeaders))
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = "";
     
            if (!TryRetrieveToken(context, out token))
            {
                await _next(context);
            }
            else
            {
                // Aquí puedes escribir tu lógica para validar el token JWT
                
                if (!string.IsNullOrEmpty(token))
                {
                    //Validación de token 
                    string _secretKey = _configuration.GetValue<string>("JwtSettings:SecretKey");
                    var key = Encoding.ASCII.GetBytes(_secretKey);
                    //validar el time life expiration

                    // Configurar los parámetros de la validación del token
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero // Sin margen de error para el tiempo
                    };

                    // Intenta validar el token
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                        // Verificar si el token ha expirado
                        if (validatedToken.ValidTo < DateTime.UtcNow)
                        {
                            context.Response.StatusCode = 401; // Unauthorized
                            await context.Response.WriteAsync("El token ha expirado");
                            return;
                        }

                        // Realizar la validación adicional del token en tu base de datos u otros criterios de negocio
                        bool tokenValido2 = ValidarTokenEnBaseDeDatos(token);

                        if (!tokenValido2)
                        {
                            context.Response.StatusCode = 401; // Unauthorized
                            await context.Response.WriteAsync("Token no válido");
                            return;
                        }
                    }
                    catch (SecurityTokenException)
                    {
                        context.Response.StatusCode = 401; // Unauthorized
                        await context.Response.WriteAsync("Token inválido");
                        return;
                    }



                    // Realizar la validación del token en tu base de datos u otros criterios de negocio                                                                                                                       
                    bool tokenValido = ValidarTokenEnBaseDeDatos(token);

                    if (!tokenValido)
                    {
                        context.Response.StatusCode = 401; // Unauthorized
                        await context.Response.WriteAsync("Token no válido");
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("Token no proporcionado");
                    return;
                }

                await _next(context);
            }
        }

        private bool ValidarTokenEnBaseDeDatos(string token)
        {
            // Aquí puedes implementar la lógica para validar el token en tu base de datos u otros criterios de negocio
            // Retorna true si el token es válido, de lo contrario retorna false
            // Por ejemplo, puedes verificar si el token está en la base de datos de tokens válidos
            return true;
        }
    }

    public static class TokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenValidationMiddleware>();
        }
    }

}



