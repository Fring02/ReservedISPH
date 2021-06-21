using Microsoft.Extensions.DependencyInjection;
using ISPH.Domain.Interfaces.Repositories;
using ISPH.Domain.Interfaces.Services;
using ISPH.Infrastructure.Data.Repositories;
using ISPH.Infrastructure.Services.Services;

namespace ISPH.API.Extensions
{
    public static class ServicesDependencies
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudentsRepository, StudentRepository>();
            services.AddScoped<IAdvertisementsRepository, AdvertisementsRepository>();
            services.AddScoped<ICompaniesRepository, CompaniesRepository>();
            services.AddScoped<IEmployersRepository, EmployersRepository>();
            services.AddScoped<IPositionsRepository, PositionsRepository>();
            services.AddScoped<IResumesRepository, ResumesRepository>();
            services.AddScoped<IFeaturedAdvertisementsRepository, FeaturedAdvertisementsRepository>();
        }
        public static void AddInteractors(this IServiceCollection services)
        {
            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<IAdvertisementsService, AdvertisementsService>();
            services.AddScoped<ICompaniesService, CompaniesService>();
            services.AddScoped<IEmployersService, EmployersService>();
            services.AddScoped<IPositionsService, PositionsService>();
            services.AddScoped<IResumesService, ResumesService>();
            services.AddScoped<IFeaturedAdvertisementsService, FeaturedAdvertisementsService>();
        }
    }
}
