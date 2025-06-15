using Seed.Application.Model;
using Seed.Cross.OperationResult;
using Seed.Infrastructure.InfraContracts;
using System.IO;

namespace Seed.Infrastructure.InfraImplementations
{
	public class FolderWriter : IFolderWriter
	{
		public async Task<OperationResult<bool>> WriteFolderAsync(Folder folder, string rootPath)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(rootPath))
					return "Root path cannot be null or empty.";
				
				if (folder == null)
					return "Folder object cannot be null.";

				var rootFolderPath = Path.Combine(rootPath, folder.FolderName);
				Directory.CreateDirectory(rootFolderPath);

				await CreateChildFoldersAsync(folder.ChildrenFolders, rootFolderPath);

				return true;
			}
			catch (Exception ex)
			{
				return ex;
			}
		}

		private async Task CreateChildFoldersAsync(IList<Folder>? childrenFolders, string parentPath)
		{
			if (childrenFolders == null || !childrenFolders.Any())
				return;

			foreach (var childFolder in childrenFolders)
			{
				var childFolderPath = Path.Combine(parentPath, childFolder.FolderName);
				Directory.CreateDirectory(childFolderPath);

				// Recursively create subfolders
				await CreateChildFoldersAsync(childFolder.ChildrenFolders, childFolderPath);
			}
		}
	}
}
