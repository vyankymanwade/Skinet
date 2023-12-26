using API.Errors;
using API.Middlewares;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extesions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration){


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DbStoreContext>(options => {
                options.UseSqlite(configuration.GetConnectionString("DefaultDbConnectionString"));
            });

            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext => {

                    IEnumerable<string> errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                    .SelectMany(e => e.Value.Errors)
                                                    .Select(e => e.ErrorMessage);

                    var apiResponse = new ApiValidationErrorResponse(){
                        Errors = errors
                    };

                    return new BadRequestObjectResult(apiResponse);
                };
            });

            services.AddCors(options =>{
                options.AddPolicy("CorsPolicy", policy =>{
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            return services;
        }
    }
}