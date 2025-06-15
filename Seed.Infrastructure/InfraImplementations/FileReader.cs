using Seed.Cross.OperationResult;
using Seed.Infrastructure.InfraContracts;
using System.Text;

namespace Seed.Infrastructure.InfraImplementations
{
	public class FileReader : IFileReader
	{
		public async Task<OperationResult<string[]>> ReadFileAsync(string filePath)
		{
			try
			{
				var content = await System.IO.File.ReadAllLinesAsync(filePath, Encoding.Default);
				return OperationResult<string[]>.FromSuccess(content);
			}
			catch (Exception ex)
			{
				return OperationResult<string[]>.FromException(ex);
			}
		}
	}
}

