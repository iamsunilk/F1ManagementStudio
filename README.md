# F1ManagementStudio

F1ManagementStudio is an ASP.NET Core Web API project built using Entity Framework Core, Identity, JWT Authentication, AutoMapper, FluentValidation, and SQL Server.

---

# Install Process

## Software Needed

1. VS Code (Purple)
2. Visual Studio (Blue)
3. Microsoft SQL Server
4. Microsoft SQL Server Management Studio (SSMS)

---

# Bug

## Facing Issue During SSMS Install

If you are facing issues while installing SSMS, follow the steps below.

## PowerShell Command

### PowerShell Command 1

``powershell
New-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Services\stornvme\Parameters\Device" -Name "ForcedPhysicalSectorSizeInBytes" -PropertyType MultiString -Force -Value "* 4095"

 powerShell Command 2

``powershell
Get-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Services\stornvme\Parameters\Device" -Name "ForcedPhysicalSectorSizeInBytes"



### After running the above commands:

Restart your machine

Install SSMS again

It will work properly.

### Packages in NuGet

```
1.Microsoft.EntityFrameworkCore.Tools

1.Microsoft.AspNetCore.Authentication.JwtBearer

1.Microsoft.AspNetCore.Identity.EntityFrameworkCore

1.Microsoft.AspNetCore.OpenApi

1.Microsoft.EntityFrameworkCore.SqlServer

1.Microsoft.EntityFrameworkCore.Sqlite

1.AutoMapper

```


```Very Important

1.AutoMapper.Extensions.Microsoft.DependencyInjection

1.FluentValidation.AspNetCore

1.Microsoft.IdentityModel.JsonWebTokens


```

### AutoMapper

Both packages below must be the SAME version:
```
1.AutoMapper

1.AutoMapper.Extensions.Microsoft.DependencyInjection

````
### Migration Command

Run the following commands in Package Manager Console:

1.Add-Migration MigrationOne -Context CarDbContext

1.Update-Database -Context CarDbContext


### JWT Token

`` Add this inside appsettings.json:
```
"Jwt": {
  "Key": "hdf78sdf7sd8f7sdf87sd8f7sdf87sdf87sdf87sdf87sdf87sdf87sdf87sdf",
  "Issuer": "https://localhost:7261/",
  "Audience": "https://localhost:7261/"
}

```
`` Validation
JWT Bearer
```
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            AuthenticationType = "Jwt",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey =
            new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]))
        };
    });
```

`` Identity Token

```
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("F1ManagementStudio")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
```

`` Password Validation

```
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});
```
`` Token Validation Box in Swagger UI

```

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(
        name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer token like: `Bearer {your JWT}`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
});
```
`` How to Run the Project

```
Install all required software

Install required NuGet packages

Configure your connection string

Run migration commands

Run the project

Open Swagger UI

Generate JWT token

Click Authorize

`` Enter:

Bearer your_generated_token
```



---

Now this README includes **everything you mentioned**, properly structured and ready to paste.

If you want, I can also make a slightly more professional GitHub-style version with badges and formatting improvements.
