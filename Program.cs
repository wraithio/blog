using System.Text;
using blog.Context;
using blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<BlogServices>();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
    policy => {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});


var secretKey = builder.Configuration["JWT:key"];
var signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

// we areadding auth to your build to chack the JWTToken from our services
builder.Services.AddAuthentication(options => 
{
    // this line of code will set the authentification behaviour of our JWt Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // sets the default behaviour for when our auth fails
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer( options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //check if the tokens issuer is valid
        ValidateAudience = true, //check if the token's audience is valid  
        ValidateLifetime = true, //ensures that our tokens haven't expired
        ValidateIssuerSigningKey = true, //checking the tokens signature is valid

        ValidIssuer = "https://robinsonblog-h6fyg9ghabbbf4a2.westus-01.azurewebsites.net/",
        ValidAudience = "https://robinsonblog-h6fyg9ghabbbf4a2.westus-01.azurewebsites.net/",
        IssuerSigningKey = signingCredentials
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
