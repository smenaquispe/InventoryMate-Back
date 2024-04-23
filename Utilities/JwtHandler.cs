namespace InventoryMate.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

public static class JwtHandler {
    public static string? Key;

    public static void Configure(string key, IServiceCollection services) {
        Key = key;
        services.AddAuthorization();
        services.AddAuthentication("Bearer").AddJwtBearer(options => {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters() {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = signingKey
            };

        });
    }

    public static string CreateToken(string id, string role = "User") {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Key!);

        var tokenDes = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, id),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDes);

        return tokenHandler.WriteToken(token);
    }
}