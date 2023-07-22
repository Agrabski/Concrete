using System.Diagnostics.CodeAnalysis;

namespace Concrete.Core.Serialization;
public interface IConcreteSerializer
{
	public string Serialize<T>(T obj);
	public T Deserialize<T>([StringSyntax(StringSyntaxAttribute.Json)] string json);
}