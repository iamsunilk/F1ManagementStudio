# F1ManagementStudio


# install process 

## Software needed

1.VS Code (Purple).
1.Visual studio (Blue).
1.Microsoft SQL Server Management studio.

# Bug
1.Facing Issue during SSMS install.

1.PowerShell Command:

# Powershell command 1
New-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Services\stornvme\Parameters\Device" -Name "ForcedPhysicalSectorSizeInBytes" -PropertyType MultiString -Force -Value "* 4095"

# Powershell command 2
Get-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Services\stornvme\Parameters\Device" -Name "ForcedPhysicalSectorSizeInBytes"


1.Restart Your machine it will be work.

# Packages in NuGet

1.Microsoft.EntityFrameworkCore.Tools
1.Microsoft.AspNetCore.Authentication.JwtBearer
1.Microsoft.AspNetCore.Identity.EntityFrameworkCore
1.Microsoft.AspNetCore.OpenApi
1.Microsoft.EntityFrameworkCore.SqlServer
1.Microsoft.EntityFrameworkCore.Sqlite
1.AutoMapper
# very Important
1.AutoMapper.Extensions.Microsoft.DependencyInjection
1.FluentValidation.AspNetCore
1.Microsoft.IdentityModel.JsonWebTokens

Automapper 

1.Both are same version

# Migration Command

1.Add-Migration MigrationOne -Context CarDbContext
1.Update-database -Context CarDbContext




# JWT Token

 "Jwt": {
   "Key": "hdf78sdf7sd8f7sdf87sd8f7sdf87sdf87sdf87sdf87sdf87sdf87sdf87sdf",
   "Issuer": "https://localhost:7261/",
   "Audience": "https://localhost:7261/"
 }



 # Validation 

 //Jwt Bearer 
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
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });




# Identity Token

    //Identity

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("F1ManagementStudio")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();




# Password Validation

    builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});



# Token Validation Box in SwaggerUI

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
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


