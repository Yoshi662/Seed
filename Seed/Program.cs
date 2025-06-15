using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Seed.Terminal.DependencyInjection;
using Seed.Domain.ServiceContracts;
using Seed.Infrastructure.InfraContracts;
using Seed.Terminal;

var hostbuilder = Host.CreateDefaultBuilder(args)
	.ConfigureServices((context, services) =>
	{
		services.RegisterDependencies();
	})
	.Build();

var seedService = hostbuilder.Services.GetRequiredService<ISeedService>();
new FileProcessor(seedService).ProcessFileAsync().GetAwaiter().GetResult();