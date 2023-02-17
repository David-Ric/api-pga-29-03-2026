
global using Microsoft.EntityFrameworkCore;
global using Microsoft.OpenApi.Models;
global using PortalGrupoAlyne.Model;
global using PortalGrupoAlyne.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using PortalGrupoAlyne.Services;
using System.Text.Json.Serialization;
using System;
using PortalGrupoAlyne.Infra.Services;
using Microsoft.Extensions.FileProviders;
using PortalGrupoAlyne.Helpers;
using PortalGrupoAlyne.Persist.Contratos;
using PortalGrupoAlyne.Persist;

namespace PortalGrupoAlyne
{
    public class Program
    {
        public static void Main(string[] args)

        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string mySqlConnection =
              builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextPool<DataContext>(options =>
                options.UseMySql(mySqlConnection,
                      ServerVersion.AutoDetect(mySqlConnection)));

            //builder.Services.AddDbContext<ProEventosContext>(
            //    context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            //);

            builder.Services.AddCors();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Portal Grupo Alyne", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<DataContext>();

            // Services

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVendedorService, VendedorService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IConcorrentesService, ConcorrenteService>();
            builder.Services.AddScoped<IProdutoConcorrenteService, ProdutoConcorrenteService>();
            builder.Services.AddScoped<IParceirosService, ParceirosService>();
            builder.Services.AddScoped<ITipoNegociacaoService, TipoNegociacaoService>();
            builder.Services.AddScoped<ITabelaPrecoService, TabelaPrecoService>();
            builder.Services.AddScoped<IItemTabelaService, ItemTabelaService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<IEmpresaService, EmpresaService>();
            builder.Services.AddScoped<IPaginaPermissaoService, PaginaPermissaoService>();
            builder.Services.AddScoped<IPaginaService, PaginaService>();
            builder.Services.AddScoped<IPaginaBaseService, PaginaBaseService>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<ISubMenuService, SubMenuService>();
            builder.Services.AddScoped<ISubMenuPermissaoService, SubMenuPermissaoService>();
            builder.Services.AddScoped<IUserGrupoService, GrupoUsuarioService>();
            builder.Services.AddScoped<IMenuPermissoesService,MenuPermissaoService>();
            builder.Services.AddScoped<ITabelaPrecoParceiroService, TabelaPrecoParceiroService>();
            builder.Services.AddScoped<ICabecalhoPedidoVendaService, CabecalhoPedidoVendaService>();
            builder.Services.AddScoped<IItemPedidoVendaService, ItemPedidoVendaService>();


            // Persist

            builder.Services.AddScoped<ITabelaPrecoPersist, TabelaPrecoPersist>();
            builder.Services.AddScoped<IItemTabelaPersist, ItemTabelaPersist>();
            builder.Services.AddScoped<IParceirosPersist,ParceirosPersist>();
            builder.Services.AddScoped<IGeralPersist, GeralPersist>();
            builder.Services.AddScoped<IVendedorPersist, VendedorPersist>();
            builder.Services.AddScoped<IPaginaPermissaoPersist, PaginaPermissaoPersist>();
            builder.Services.AddScoped<IUsuarioPersist,UsuarioPersist>();
            builder.Services.AddScoped<IMenuPersist,MenuPersist>();
            builder.Services.AddScoped<ISubMenuPersist,SubMenuPersist>();
            builder.Services.AddScoped<ISubMenuPermissaoPersist,SubMenuPermissaoPersist>();
            builder.Services.AddScoped<IUserGrupoPersist,GrupoUsuarioPersist>();
            builder.Services.AddScoped<IPaginaPersist, PaginaPersist>();
            builder.Services.AddScoped<IMenuPermissoesPersist, MenuPermissaoPersist>();
            builder.Services.AddScoped<IProdutoPersist, ProdutoPersist>();
            builder.Services.AddScoped<ITabelaPrecoParceiroPersist, TabelaPrecoParceiroPersist>();
            builder.Services.AddScoped<ICabecalhoPedidoVendaPersist, CabecalhoPedidoVendaPersist>();
            builder.Services.AddScoped<IItemPedidoVendaPersist, ItemPedidoVendaPersist>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();


            app.UseAuthentication();


            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin());

            

            app.MapControllers();

            app.Run();
        }
    }
}