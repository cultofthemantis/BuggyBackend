using BuggyBackend.Repositories;
using BuggyBackend.Services;

var builder = WebApplication.CreateBuilder(args);

   builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMemberRepository, MemberRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();

builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IMemberService, MemberService>();
builder.Services.AddSingleton<ILibraryService, LibraryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
