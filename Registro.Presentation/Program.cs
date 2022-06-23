using Microsoft.AspNetCore.Authentication.Cookies;
using RegistroWeb.Infra.Data.Interfaces;
using RegistroWeb.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

//configurando o projeto para MVC
builder.Services.AddControllersWithViews();

//habilitando o uso de sessões no projeto
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//habilitando o projeto para usar permissão de acesso
builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

//capturar a connectionstring mapeada no 'appsettings.json'
var connectionString = builder.Configuration.GetConnectionString("BDRegistroWeb");

//injeção de dependencia para as classes do repositorio
builder.Services.AddTransient<IPessoaRepository>(map => new PessoaRepository(connectionString));
builder.Services.AddTransient<IUsuarioRepository>(map => new UsuarioRepository(connectionString));

var app = builder.Build();

//habilitando o uso de sessões no projeto
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//autenticação e autorização
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

//definindo a página inicial do projeto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}"
    );

app.Run();
