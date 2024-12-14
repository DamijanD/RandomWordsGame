using RandomWordsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/words/{cnt, minLen, maxLen}", (int cnt, int minLen, int maxLen) =>
{
    return Words.Instance.GetWords(cnt, minLen, maxLen);
})
.WithName("GetRandomWords")
.WithOpenApi();

app.Run();

