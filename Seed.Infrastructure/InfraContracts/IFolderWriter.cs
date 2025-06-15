using Seed.Application.Model;
using Seed.Cross.OperationResult;

namespace Seed.Infrastructure.InfraContracts
{
	public interface IFolderWriter
	{
		Task<OperationResult<bool>> WriteFolderAsync(Folder folder, string rootPath);
	}
}
