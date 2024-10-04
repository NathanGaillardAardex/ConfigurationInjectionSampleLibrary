using ConfigurationInjectionSampleLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseConfigurationInjectionSample(builder.Configuration);
/* or
builder.Services.AddLibraryConfiguration(builder.Configuration);
builder.Services.AddLibraryDependencies();
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test",
        (IDependency dependency, IAnotherDependency anotherDependency) =>
            $"{dependency.WriteOneMessage()}\n{anotherDependency.WriteAnotherMessage()}")
    .WithName("Test")
    .WithOpenApi();

app.Run();