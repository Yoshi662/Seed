using Seed.Domain.ServiceContracts;
using Seed.Cross.OperationResult;


namespace Seed.Terminal
{
	public class FileProcessor(ISeedService seedService)
	{

		public async Task ProcessFileAsync()
		{
			Console.WriteLine("Please enter the file path:");
			var filePath = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(filePath))
			{
				Console.WriteLine("Invalid file path. Please try again.");
				return;
			}

			var result = await seedService.CreateFolderStructureAsync(filePath);

			if (result.Success)
			{
				Console.WriteLine("Folder structure created successfully.");
			}
			else
			{
				Console.WriteLine("Failed to create folder structure. Errors:");
				Console.WriteLine(result.ToDebugString());
			}
		}

	}
}