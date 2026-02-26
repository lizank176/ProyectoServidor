using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;

var builder = WebApplication.CreateBuilder(args);

// --- SECCIÓN 1: REGISTRO DE SERVICIOS (builder.Services) ---

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Necesario para Identity

// 1. Configurar la Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Configurar Identity (Registro/Login y Cookies)
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false; // Para facilitar tus pruebas
    options.Password.RequiredLength = 4;
})
.AddRoles<IdentityRole>() // Para gestión de permisos
.AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Gestión de Datos de Sesión (Punto importante de tu lista)
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build(); // <--- AQUÍ SE CREA LA VARIABLE 'app'

// --- SECCIÓN 2: CONFIGURACIÓN DEL PIPELINE (app.Use...) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Gestión de errores
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Para que funcionen CSS, JS y las fotos que subas

app.UseRouting();

// 4. Autenticación y Sesión (El orden es vital)
app.UseAuthentication(); // Gestiona quién eres (Cookies)
app.UseAuthorization();  // Gestiona qué puedes hacer (Permisos)
app.UseSession();        // Habilita HttpContext.Session

// --- SECCIÓN 3: MAPEO DE RUTAS (app.Map...) ---

// Ruta por defecto (Punto: Rutas estáticas)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Recetas}/{action=Index}/{id?}");

app.MapRazorPages(); // Necesario para las vistas de Identity (Login/Registro)

app.Run();