using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Cross.OperationResult
{
	/// <summary>
	/// Represents an exception that contains only a message and no additional data.
	/// </summary>
	public class MessageOnlyException : Exception {
		public MessageOnlyException(string ErrorMessage) : base(ErrorMessage)
		{
		}
	}
}
