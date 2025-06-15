using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Cross.OperationResult
{
	public static class OperationResultExtensions
	{
		public static string GetFirstExceptionMessage<T>(this OperationResult<T> operationResult)
		{
			if (operationResult.Exceptions.Count == 0)
				return string.Empty;

			var exception = operationResult.Exceptions[0];
			return $"msg: {exception.Message}" +
				$"\nStackTrace:{exception.StackTrace}";
		}

		public static string ToDebugString<T>(this OperationResult<T> result)
		{
			var sb = new StringBuilder();
			sb.AppendLine("=== OperationResult Debug ===");
			sb.AppendLine($"Success: {result.Success}");
			sb.AppendLine($"Type: {typeof(T).FullName}");

			if (result.Success)
			{
				sb.AppendLine("Data:");
				sb.AppendLine(result.Result?.ToString() ?? "  <null>");
			}
			else
			{
				sb.AppendLine($"Exception Count: {result.Exceptions.Count}");
				for (int i = 0; i < result.Exceptions.Count; i++)
				{
					var ex = result.Exceptions[i];
					sb.AppendLine($"--- Exception #{i + 1} ---");
					sb.AppendLine($"Type: {ex.GetType().FullName}");
					sb.AppendLine($"Message: {ex.Message}");
					sb.AppendLine("StackTrace:");
					sb.AppendLine(ex.StackTrace ?? "  <no stack trace>");
				}
			}

			sb.AppendLine("=== End Debug ===");
			return sb.ToString();
		}

		public static string ToDebugStringCompact<T>(this OperationResult<T> result)
		{
			if (result.Success)
			{
				return $"[Success] Type: {typeof(T).Name}, Data: {result.Result?.ToString() ?? "<null>"}";
			}
			else
			{
				var messages = string.Join(" | ", result.Exceptions.Select(e => $"{e.GetType().Name}: {e.Message}"));
				return $"[Failure] Type: {typeof(T).Name}, Exceptions: {messages}";
			}
		}
	}
}
