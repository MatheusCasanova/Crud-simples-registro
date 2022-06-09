using RegistroWeb.Infra.Data.Interfaces;
using RegistroWeb.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

//configurando o projeto para MVC
builder.Services.AddControllersWithViews();

//capturar a connectionstring mapeada no 'appsettings.json'
var connectionString = builder.Configuration.GetConnectionString("BDRegistroWeb");

//enviar a connectionString para a classe PessoaRepository
builder.Services.AddTransient<IPessoaRepository>
    (map => new PessoaRepository(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//definindo a página inicial do projeto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );

app.Run();
