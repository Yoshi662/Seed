using Seed.Cross.OperationResult;
using Seed.Infrastructure.InfraContracts;
using Seed.Infrastructure.InfraImplementations;

namespace Seed.Infrastructure.UnitTest
{
	public class FileReaderUnitTest
	{
		private readonly IFileReader _fileReader = new FileReader();

		[Fact]
		public async Task ReadFileAsync_ReturnsFileContent_WhenFileExists()
		{
			// Arrange
			var filePath = Path.GetTempFileName();
			string[] expectedContent = [@"^(?<indent>[\s│¦]*)(├───|└───|\\+---)(?<name>.+)$"];
			await File.WriteAllLinesAsync(filePath, expectedContent);

			// Act
			var result = await _fileReader.ReadFileAsync(filePath);

			// Assert
			Assert.True(result.Success);
			Assert.NotNull(result.Result);
			Assert.Equal(expectedContent, result.Result);

			// Cleanup
			File.Delete(filePath);
		}

		[Fact]
		public async Task ReadFileAsync_ReturnsException_WhenFileDoesNotExist()
		{
			// Arrange
			var filePath = Path.Combine(Path.GetTempPath(), "nonexistentfile.txt");

			// Act
			var result = await _fileReader.ReadFileAsync(filePath);

			// Assert
			Assert.False(result.Success);
			Assert.NotNull(result.Exceptions);
			Assert.IsType<FileNotFoundException>(result.Exceptions.First());
		}

		[Fact]
		public async Task ReadFileAsync_ReturnsException_WhenFilePathIsInvalid()
		{
			// Arrange
			var invalidFilePath = "InvalidPath<>";

			// Act
			var result = await _fileReader.ReadFileAsync(invalidFilePath);

			// Assert
			Assert.False(result.Success);
			Assert.NotNull(result.Exceptions);
			Assert.IsType<IOException>(result.Exceptions.First());
		}
	}
}
