using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Cross.OperationResult
{
	public class OperationResult<T>
	{
		public bool Success => Result is not null && !Exceptions.Any();
		public T? Result { get; init; }
		public List<Exception> Exceptions = [];

		public static implicit operator OperationResult<T>(T data)
			=> new() { Result = data };


		public static implicit operator OperationResult<T>(Exception exception)
			=> new() { Exceptions = [exception] };

		public static implicit operator OperationResult<T>(List<Exception> exceptions)
			=> new() { Exceptions = exceptions };

		public static implicit operator OperationResult<T>(string errorMessage)
			=> new() { Exceptions = [new MessageOnlyException(errorMessage)] };

		public static OperationResult<T> FromSuccess(T data) => new() { Result = data };
		public static OperationResult<T> FromException(Exception ex) => new() { Exceptions = [ex] };
		public static OperationResult<T> FromError(string message) => new() { Exceptions = [new MessageOnlyException(message)] };
	}
}

