using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.API;
using SchoolManagement.Data;
using SchoolManagement.Models.Constants;

static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddIdentityCore<IdentityUser<int>>(o =>
        {
            o.Stores.MaxLengthForKeys = 128;
        })
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.CanViewInfo, policy =>
            {
                policy.RequireClaim(ClaimNames.Permissions, Permissions.CanViewInfo);
            });
        });


        return services;
    }
}
