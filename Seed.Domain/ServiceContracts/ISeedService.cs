using Seed.Application.Model;
using Seed.Cross.OperationResult;

namespace Seed.Domain.ServiceContracts
{
	public interface ISeedService
	{
		ValueTask<OperationResult<bool>> CreateFolderStructureAsync(string filepath);
	}
}
