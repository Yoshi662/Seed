using Seed.Application.Model;
using Seed.Application.ServiceContracts;
using Seed.Domain.ServiceImplementations;

namespace Seed.Domain.UnitTest
{
	public class FolderParserUnitTest
	{
		private readonly IFolderParser _parser = new FolderParser();
		[Fact]
		public async Task FolderParseOK()
		{
			// Arrange
			List<string> input = [
@"C:\TEMP\ROOT",
@"+---Folder1",
@"¦   +---Folder3",
@"+---Folder2"];

			// Act
			var result = await _parser.ParseStringToFolderAsync(input.ToArray());

			// Assert
			Assert.True(result.Success);
			Assert.NotNull(result.Result);

			var root = result.Result!;
			Assert.Equal("C:\\TEMP\\ROOT", root.FolderName);
			Assert.NotNull(root.ChildrenFolders);

			var children = new List<Folder>(root.ChildrenFolders!);
			Assert.Equal(2, children.Count);

			Assert.Equal("Folder1", children[0].FolderName);
			Assert.Single(children[0].ChildrenFolders!);
			Assert.Equal("Folder3", new List<Folder>(children[0].ChildrenFolders!)[0].FolderName);

			Assert.Equal("Folder2", children[1].FolderName);
			Assert.True(!children[1].ChildrenFolders.Any());
		}


		[Fact]
		public async Task ParseStringToFolderAsync_ParsesDeepTreeCorrectly()
		{
			// Arrange
			List<string> input  = [
@"C:\TEMP\ROOT",
@"+---Folder1",
@"¦   +---Folder3",
@"+---Folder2",
@"¦   +---Folder3",
@"¦   ¦   +---Folder4",
@"¦   +---Folder5",
@"¦   +---Folder6",
@"¦   +---Folder7",
@"+---Folder8"];

			// Act
			var result = await _parser.ParseStringToFolderAsync(input.ToArray());

			// Assert
			Assert.True(result.Success);
			var root = result.Result!;
			Assert.Equal("C:\\TEMP\\ROOT", root.FolderName);

			var rootChildren = root.ChildrenFolders!.ToList();
			Assert.Equal(3, rootChildren.Count); // Folder1, Folder2, Folder8

			var folder1 = rootChildren[0];
			Assert.Equal("Folder1", folder1.FolderName);
			Assert.Single(folder1.ChildrenFolders!);
			Assert.Equal("Folder3", folder1.ChildrenFolders!.First().FolderName);

			var folder2 = rootChildren[1];
			Assert.Equal("Folder2", folder2.FolderName);

			var folder2Children = folder2.ChildrenFolders!.ToList();
			Assert.Equal(4, folder2Children.Count);

			// Folder3 (with child Folder4)
			var folder2_3 = folder2Children[0];
			Assert.Equal("Folder3", folder2_3.FolderName);
			Assert.Single(folder2_3.ChildrenFolders!);
			Assert.Equal("Folder4", folder2_3.ChildrenFolders!.First().FolderName);

			Assert.Equal("Folder5", folder2Children[1].FolderName);
			Assert.True(!folder2Children[1].ChildrenFolders.Any());

			Assert.Equal("Folder6", folder2Children[2].FolderName);
			Assert.True(!folder2Children[2].ChildrenFolders.Any());

			Assert.Equal("Folder7", folder2Children[3].FolderName);
			Assert.True(!folder2Children[3].ChildrenFolders.Any());

			var folder8 = rootChildren[2];
			Assert.Equal("Folder8", folder8.FolderName);
			Assert.True(!folder8.ChildrenFolders.Any());
		}
	}
}