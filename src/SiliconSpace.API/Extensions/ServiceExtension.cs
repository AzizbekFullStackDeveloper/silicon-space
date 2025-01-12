using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Data.Repositories;
using SiliconSpace.Service.Interfaces;
using SiliconSpace.Service.Mappings;
using SiliconSpace.Service.Services;
using System.Reflection;
using System.Text;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace SiliconSpace.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ICoworkingService, CoworkingService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICoworkingZoneService, CoworkingZoneService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IGuidGenerator, GuidGenerator>();


            services.AddAutoMapper(typeof(Mapping));

            //Registration
            services.AddHttpClient();
            /*
                        services.AddScoped<ISmsService, SmsService>();
                        services.AddScoped<IRegistrationService, RegistrationService>();
            */
            services.AddFusionCache()
               .WithOptions(options => {
                   options.DistributedCacheCircuitBreakerDuration = TimeSpan.FromSeconds(2);
               })
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromMinutes(1),

                    IsFailSafeEnabled = true,
                    FailSafeMaxDuration = TimeSpan.FromHours(2),
                    FailSafeThrottleDuration = TimeSpan.FromSeconds(5),

                    FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
                    FactoryHardTimeout = TimeSpan.FromMilliseconds(1500),

                    // DISTRIBUTED CACHE OPTIONS
                    DistributedCacheSoftTimeout = TimeSpan.FromSeconds(1),
                    DistributedCacheHardTimeout = TimeSpan.FromSeconds(2),
                    AllowBackgroundDistributedCacheOperations = true
                })
                .WithSerializer(
                    new FusionCacheNewtonsoftJsonSerializer(
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
                )
                .WithDistributedCache(
                    new RedisCache(
                        new RedisCacheOptions()
                        {
                            Configuration = "localhost:6379,password=team123" 
                        })
                )
            ;

            services.AddMemoryCache();
        }


        public static void AddRateLimiterService(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuration for rate limiting from appsettings.json
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            // Rate limiting services
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // Explicitly register an async processing strategy for rate limiting
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

        }
        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SiliconSpace.Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
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
                new string[]{ }
            }
        });
            });
        }

    }
}
