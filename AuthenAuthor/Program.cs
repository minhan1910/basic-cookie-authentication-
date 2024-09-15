using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;    
})
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;

        options.Cookie = new CookieBuilder
        {
            Name = "minhan-cookie",
        };
        options.LoginPath = "/api/authen/unauthorized";
        options.LogoutPath = "/api/authen/logout";
        options.AccessDeniedPath = "/api/authen/forbidden";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
