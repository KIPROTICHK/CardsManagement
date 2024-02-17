
using CardsManagement.API.CustomClass.Seed;
using CardsManagement.Application.Repositories;
using CardsManagement.Application.Service.Interface;
using CardsManagement.Domain;
using CardsManagement.Infrastructure;
using CardsManagement.Infrastructure.Data;
using CardsManagement.Infrastructure.Repositories;
using CardsManagement.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;
using static CardsManagement.Application.SharedDefinedValues;

var builder = WebApplication.CreateBuilder(args);

//context for models
builder.Services.AddDbContext<CoreContext>(options =>
      options.UseSqlServer(
          builder.Configuration.GetConnectionString(SharedDefinedValues.DefaultConnection)
          ));

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.SignIn.RequireConfirmedAccount = true;
})
            .AddEntityFrameworkStores<CoreContext>()
            .AddDefaultTokenProviders();


//builder.Services.AddControllers();

builder.Services.AddControllers(options => options.OutputFormatters.RemoveType<StringOutputFormatter>())
       .ConfigureApiBehaviorOptions(options =>
       {
           options.SuppressModelStateInvalidFilter = true;
       })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); 

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    c.OperationFilter<AuthResponsesOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

       .AddJwtBearer(options =>
       {

           options.Audience = "localhost";
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               ValidateIssuer = true,
               ValidIssuer = "localhost",
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["CtSettings:TokenKey"]))
           };
       });
// Add services to the container.

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
     JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder =
    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

    #region User
    options.AddPolicy(Permissions.User, builder =>
    {
        builder.AddRequirements(new PermissionRequirement(new List<PermissionItem>
                        {
                         new PermissionItem
                         {
                             Value = Permissions.User,
                             Type =PermissionType.role
                         },


                        }));

    });

    #endregion
    #region Admin
    options.AddPolicy(Permissions.Admin, builder =>
    {
        builder.AddRequirements(new PermissionRequirement(new List<PermissionItem>
                        {
                         new PermissionItem
                         {
                             Value = Permissions.Admin,
                             Type =PermissionType.role
                         },


                        }));

    });

    #endregion

});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<CoreContext>(options =>
    options.UseSqlServer(connectionString));

#region basic settings

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();


//email configurations
builder.Services.Configure<SeedAccountSettings>(builder.Configuration.GetSection(nameof(SeedAccountSettings)));
builder.Services.AddSingleton<ISeedAccountSettings>(sp =>
sp.GetRequiredService<IOptions<SeedAccountSettings>>().Value);

#endregion
#region Data or repositories
builder.Services.AddTransient<ICustomBaseRepository, CustomBaseRepository>();
builder.Services.AddTransient<IDataRepository, DataRepository>();

#endregion

#region Services
builder.Services.AddTransient<ISeedDataService, SeedDataService>();
builder.Services.AddTransient<IDataService, DataService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1"); 
        c.DefaultModelExpandDepth(0);
        c.DefaultModelsExpandDepth(-1);
         c.RoutePrefix = "";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); 
    });
}


app.UseStatusCodePages(); //for showing errors  

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

_ = Task.Run(async () => await app.SeedAccount());

app.MapControllers();

app.Run();
