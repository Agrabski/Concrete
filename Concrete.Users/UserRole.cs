using System.Text.Json.Serialization;

namespace Concrete.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
	Admin,
	Teacher,
	TeachingAssistant,
	Student
}
