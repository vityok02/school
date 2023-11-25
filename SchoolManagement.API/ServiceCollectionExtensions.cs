using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.API.Constants;
using SchoolManagement.Data;

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
            options.AddPolicy(Policies.SystemAdmin, policy =>
            {
                policy.RequireClaim(ClaimNames.Permissions, Permissions.CanManageSchool);
                policy.RequireClaim(ClaimNames.Permissions, Permissions.CanManagePositions);
                policy.RequireClaim(ClaimNames.Permissions, Permissions.CanManageSchoolAdmin);
            });
        });


        return services;
    }
}
