OUTPUT                 =========    Login/Logout Using IdentityUser / IdentityRole  ==============
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/57a8e49d-faa0-40b2-9270-f79c92ba8b11" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/bd0330b7-926f-4335-94c4-70bfe405d03f" />

//And write the following code Bearer testing Boxxx
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "JWTAuthDemo", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Seed Roles
using (var scope = app.Services.CreateScope())
{
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await rolemanager.RoleExistsAsync("User"))
    {
        await rolemanager.CreateAsync(new IdentityRole("User"));
    }
    if (!await rolemanager.RoleExistsAsync("Admin"))
    {
        await rolemanager.CreateAsync(new IdentityRole("Admin"));
    }
}

app.Run();
