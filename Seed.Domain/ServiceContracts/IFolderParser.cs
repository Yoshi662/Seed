using Seed.Application.Model;
using Seed.Cross.OperationResult;

namespace Seed.Application.ServiceContracts
{
	/// <summary>
	/// Defines a contract for parsing a string representation of a folder tree view into a <see cref="Folder"/> object.
	/// </summary>
	public interface IFolderParser
	{
		/// <summary>
		/// Parses the specified string representing a folder tree view and returns an <see cref="OperationResult{T}"/> containing a <see cref="Folder"/> object.
		/// </summary>
		/// <param name="folderTreeView">The string representation of the folder tree view to parse.</param>
		/// <returns>
		/// A <see cref="ValueTask{TResult}"/> that represents the asynchronous operation. 
		/// The task result contains an <see cref="OperationResult{T}"/> with the parsed <see cref="Folder"/> object if successful; otherwise, contains error information.
		/// </returns>
		ValueTask<OperationResult<Folder>> ParseStringToFolderAsync(string[] folderTreeView);
	}
}
