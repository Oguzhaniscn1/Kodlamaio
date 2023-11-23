using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDAL, EfProductDAL>();
builder.Services.AddControllers();
builder.Services.AddCors();


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
 });

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

//Core.Utilities.IoC.ServiceTool.Create(builder.Services);


//autofac IoC yapýlanmasýný kurudk ve tanýttýk.
builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory())
           .ConfigureContainer<ContainerBuilder>
           (
    builder => { builder.RegisterModule(new AutofacBusinessModule()); }
            );














// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();
app.UseCors(builder=>builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();




app.MapControllers();

app.Run();
