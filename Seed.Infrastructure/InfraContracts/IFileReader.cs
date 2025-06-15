using Seed.Cross.OperationResult;

namespace Seed.Infrastructure.InfraContracts
{
	public interface IFileReader
	{
		public Task<OperationResult<string[]>> ReadFileAsync(string filePath);
	}
}
