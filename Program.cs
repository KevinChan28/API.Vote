using Api.Client.MarquetStore.Security;
using API.UsersVote.Commons.Enums;
using API.UsersVote.Context;
using API.UsersVote.Hubs;
using API.UsersVote.Repository;
using API.UsersVote.Repository.Imp;
using API.UsersVote.Service;
using API.UsersVote.Service.Imp;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

const string connectionName = "VoteDB";
var connectionString = builder.Configuration.GetConnectionString(connectionName);
builder.Services.AddDbContext<VotedbContext>(options => options.UseMySQL(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiTeacherZeleris", Version = "v1" });
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	//define security for authorization
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using bearer scheme"
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
					   new string[]{}
					}

				 });
});

//Configuration of server
builder.Services.AddJwtServices(builder.Configuration);
//Roles
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("peopleOnlyPolicy", policy => policy.RequireClaim("peopleOnly", nameof(Role.Persona)));
	options.AddPolicy("AdminOrPrincipal", policy =>
		 policy.RequireRole(nameof(Role.Administrador), nameof(Role.Persona)));
});

builder.Services.AddTransient<IUserRepository, ImpUserRepository>();
builder.Services.AddTransient<IUser, ImpUser>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IVoteRepository, ImpVoteRepository>();
builder.Services.AddTransient<IVote, ImpVote>();
builder.Services.AddTransient<IPartyPoliticRepository, ImpPoliticPartyRepository>();
builder.Services.AddTransient<IPoliticParty, ImpPoliticParty>();
builder.Services.AddTransient<IAuthorization, ImpAuthorization>();
builder.Services.AddTransient<IDocumentRepository, ImpDocumentsRepository>();
builder.Services.AddTransient<IDocuments, ImpDocument>();
builder.Services.AddSignalR();

//CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "Cors", builder =>
	{
		builder.WithOrigins("http://localhost:4200");
		builder.AllowAnyMethod();
		builder.AllowAnyHeader();
		builder.AllowCredentials();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapHub<VoteHub>("/vote");

app.MapControllers();

app.Run();
