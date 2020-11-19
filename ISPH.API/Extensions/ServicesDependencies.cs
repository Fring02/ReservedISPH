using ISPH.Core.Models;
using ISPH.Infrastructure.Repositories;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ISPH.Core.Interfaces.Authentification;

namespace ISPH.API.Extensions
{
    public static class ServicesDependencies
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudentsRepository, StudentRepository>();
            services.AddScoped<IUserAuthentification<Student>, StudentRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAdvertisementsRepository, AdvertisementsRepository>();
            services.AddScoped<ICompanyRepository, CompaniesRepository>();
            services.AddScoped<IEmployersRepository, EmployersRepository>();
            services.AddScoped<IUserAuthentification<Employer>, EmployersRepository>();
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<ICompanyRepository, CompaniesRepository>();
            services.AddScoped<IPositionsRepository, PositionsRepository>();
            services.AddScoped<IResumesRepository, ResumesRepository>();
            services.AddScoped<IFavouritesRepository, FavouritesRepository>();
        }
    }
}
