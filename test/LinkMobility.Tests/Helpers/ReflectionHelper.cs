using System.Reflection;
using System.Threading.Tasks;

namespace LinkMobility.Tests.Helpers
{
	internal static class ReflectionHelper
	{
		public static T GetPrivateField<T>(object obj, string fieldName)
		{
			var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (field == null)
				throw new ArgumentException($"Field '{fieldName}' not found in type '{obj.GetType().FullName}'.");

			return (T)field.GetValue(obj);
		}

		public static void SetPrivateField<T>(object obj, string fieldName, T value)
		{
			var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (field == null)
				throw new ArgumentException($"Field '{fieldName}' not found in type '{obj.GetType().FullName}'.");

			field.SetValue(obj, value);
		}

		public static async Task<TResult> InvokePrivateMethodAsync<TResult>(object obj, string methodName, params object[] parameters)
		{
			var method = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (method == null)
				throw new ArgumentException($"Method '{methodName}' not found in type '{obj.GetType().FullName}'.");

			// If the method is a generic method definition, construct it with concrete type arguments.
			if (method.IsGenericMethodDefinition)
			{
				var genericArgs = method.GetGenericArguments();
				var typeArgs = new Type[genericArgs.Length];

				// First generic argument is the return type (TResult)
				if (typeArgs.Length > 0)
					typeArgs[0] = typeof(TResult);

				// For additional generic arguments (e.g., TRequest) try to infer from provided parameters
				if (typeArgs.Length > 1)
				{
					// Common pattern: second generic parameter corresponds to the second method parameter (index 1)
					Type inferred = typeof(object);
					if (parameters.Length > 1 && parameters[1] != null)
						inferred = parameters[1].GetType();

					for (int i = 1; i < typeArgs.Length; i++)
						typeArgs[i] = inferred;
				}

				method = method.MakeGenericMethod(typeArgs);
			}

			var task = (Task)method.Invoke(obj, parameters);
			await task.ConfigureAwait(false);

			var resultProperty = task.GetType().GetProperty("Result");
			return (TResult)resultProperty.GetValue(task);
		}
	}
}
