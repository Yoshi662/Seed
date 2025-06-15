using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Application.Model
{
	public record Folder(string FolderName, IList<Folder>? ChildrenFolders);
}
