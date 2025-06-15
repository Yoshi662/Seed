using Seed.Application.Model;
using Seed.Application.ServiceContracts;
using Seed.Cross.OperationResult;
using Seed.Domain.ServiceContracts;
using Seed.Infrastructure.InfraContracts;

namespace Seed.Domain.ServiceImplementations
{
	public class SeedService(IFolderParser folderParser, IFolderWriter folderWriter, IFileReader fileReader) : ISeedService
	{
		public async ValueTask<OperationResult<bool>> CreateFolderStructureAsync(string filepath)
		{
			var contentResult = await fileReader.ReadFileAsync(filepath);

			if (!contentResult.Success)
				return contentResult.Exceptions;

			var rootResult = await folderParser.ParseStringToFolderAsync(contentResult.Result);

			if (!rootResult.Success)
				return rootResult.Exceptions;
			ArgumentNullException.ThrowIfNull(rootResult.Result, nameof(rootResult.Result));

			var writeFolderResult = await folderWriter.WriteFolderAsync(rootResult.Result, filepath);

			return writeFolderResult;
		}
	}
}
