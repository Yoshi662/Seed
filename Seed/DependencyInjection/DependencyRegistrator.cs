using Microsoft.Extensions.DependencyInjection;
using Seed.Application.ServiceContracts;
using Seed.Domain.ServiceContracts;
using Seed.Domain.ServiceImplementations;
using Seed.Infrastructure.InfraContracts;
using Seed.Infrastructure.InfraImplementations;

namespace Seed.Terminal.DependencyInjection
{
	public static class DependencyRegistrator
	{
		public static void RegisterDependencies(this IServiceCollection services)
		{
			//Domain services
			services.AddTransient<IFolderParser, FolderParser>();
			services.AddTransient<ISeedService, SeedService>();

			//Infrastructure services
			services.AddTransient<IFileReader, FileReader>();
			services.AddTransient<IFolderWriter, FolderWriter>();

		}
	}
}
