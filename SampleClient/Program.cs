using MessageWriterLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the library services to the container, client can stay ignorant of the library's configuration, and inner dependencies
builder.Services.AddMessageWriterLibrary(builder.Configuration.GetMessageWriterOptions());
// This doesn't stop the client from adding the services manually, but it's not necessary anymore

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test",
        (IFirstMessageWriter dependency, ISecondMessageWriter anotherDependency) =>
            $"{dependency.FirstMessage()}\n{anotherDependency.SecondMessage()}")
    .WithName("Test")
    .WithOpenApi();

app.Run();