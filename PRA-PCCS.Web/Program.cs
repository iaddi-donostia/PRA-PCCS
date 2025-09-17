using PRA_PCCS.Web;
using PRA_PCCS.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// === DI ra�z (m�nimo por ahora) ===
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// === Construcci�n de la app ===
var app = builder.Build();

// Nada de HTTPS redirection/HSTS para evitar avisos en entornos sin cert
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();

