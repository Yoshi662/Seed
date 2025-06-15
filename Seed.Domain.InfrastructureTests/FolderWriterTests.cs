using Seed.Application.Model;
using Seed.Cross.OperationResult;
using Seed.Infrastructure.InfraImplementations;
using System.IO;

namespace Seed.Infrastructure.Tests
{
	public class FolderWriterTests
	{
		[Fact]
		public async Task WriteFolderAsync_CreatesFolderStructureCorrectly()
		{
			// Arrange
			var folderWriter = new FolderWriter();
			var rootFolder = new Folder("RootFolder", new List<Folder>
			{
				new Folder("ChildFolder1", new List<Folder>
				{
					new Folder("SubChildFolder1", null)
				}),
				new Folder("ChildFolder2", null)
			});

			var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Directory.CreateDirectory(tempDirectory);

			try
			{
				// Act
				var result = await folderWriter.WriteFolderAsync(rootFolder, tempDirectory);

				// Assert
				Assert.True(result.Success);
				Assert.True(Directory.Exists(Path.Combine(tempDirectory, "RootFolder")));
				Assert.True(Directory.Exists(Path.Combine(tempDirectory, "RootFolder", "ChildFolder1")));
				Assert.True(Directory.Exists(Path.Combine(tempDirectory, "RootFolder", "ChildFolder1", "SubChildFolder1")));
				Assert.True(Directory.Exists(Path.Combine(tempDirectory, "RootFolder", "ChildFolder2")));
			}
			finally
			{
				// Cleanup
				if (Directory.Exists(tempDirectory))
				{
					Directory.Delete(tempDirectory, true);
				}
			}
		}
	}
}