﻿namespace Concrete.Users;
public class User
{
	public Guid Id { get; init; }
	public required string Name { get; set; }
	public required string Surname { get; set; }
}