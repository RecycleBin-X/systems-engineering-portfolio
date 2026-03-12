// Add controllers
services.AddControllers();

// Add Swagger/OpenAPI
services.AddSwaggerGen();

var envVar = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (envVar == null)
	return;

var applicationUrl = envVar.Split(";");
var secretKey = _configuration["SecretKey"]!;

// Authentication
services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = "https://xxxxx.com",
		ValidAudience = "https://api.xxxxx.com",
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
	};
});

// Add CORS
services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", builder =>
	{
		var allOrigins = new string[] { "*" };
		var allowedHost = _configuration.GetSection("AllowedHosts").Get<string[]>();
		
		builder.WithOrigins(allowedHost!.Length == 0 ? allOrigins : allowedHost)
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});
