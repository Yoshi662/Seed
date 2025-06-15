using Seed.Application.Model;
using Seed.Application.ServiceContracts;
using Seed.Cross.OperationResult;
using System.Text.RegularExpressions;


namespace Seed.Domain.ServiceImplementations
{
	public class FolderParser() : IFolderParser
	{
		public ValueTask<OperationResult<Folder>> ParseStringToFolderAsync(string[] folderTreeView)
		{
			try
			{
				if (folderTreeView.Length == 0)
					return ValueTask.FromResult(OperationResult<Folder>.FromError("Empty input."));

				var root = ParseLinesToFolder(folderTreeView.ToList());
				return ValueTask.FromResult(OperationResult<Folder>.FromSuccess(root));
			}
			catch (Exception ex)
			{
				return ValueTask.FromResult(OperationResult<Folder>.FromError($"Parsing failed: {ex.Message}"));
			}
		}

		private Folder ParseLinesToFolder(List<string> lines)
		{
			var rootLine = lines[0].Trim();
			var rootFolder = new Folder(rootLine, []);

			var stack = new Stack<(int indent, Folder folder)>();
			stack.Push((0, rootFolder));

			var folderLinePattern = new Regex(@"^(?<indent>[\s│¦�]*)(├───|└───|\+---)(?<name>.+)$");

			for (int i = 1; i < lines.Count; i++)
			{
				var line = lines[i];
				var match = folderLinePattern.Match(line);
				if (!match.Success)
					continue;

				var indentString = match.Groups["indent"].Value;
				var folderName = match.Groups["name"].Value.Trim();
				int indentLevel = indentString.Count(c => c == '│' || c == ' ' || c == '¦' || c == '�');

				var newFolder = new Folder(folderName, []);

				while (stack.Count > 1 && stack.Peek().indent >= indentLevel)
					stack.Pop();

				var parent = stack.Peek();
				((IList<Folder>)parent.folder.ChildrenFolders!).Add(newFolder);
				stack.Push((indentLevel, newFolder));
			}

			return rootFolder;
		}
	}
}

