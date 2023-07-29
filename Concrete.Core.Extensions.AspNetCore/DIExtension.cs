﻿using Concrete.Core.Extensions.AspNetCore.Auth;
using Concrete.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Core.Extensions.AspNetCore;
public static class DIExtension
{
	public static IServiceCollection ConfigureConcreteSerialization(this IServiceCollection collection) =>
		collection.ConfigureOptions<JsonOptionsConfiguration>();

	public static WebApplicationBuilder ConfigureConcreteAuthentication(
		this WebApplicationBuilder builder,
		Action<JwtBearerOptions> configureJwt,
		Action<CookieAuthenticationOptions> configureCookies
	)
	{
		builder
			.Services
			.AddTransient<PasswordHasher<IAuthenticatedUser>>()
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(
				JwtBearerDefaults.AuthenticationScheme,
				configureJwt
			)
			.AddCookie(
				CookieAuthenticationDefaults.AuthenticationScheme,
				configureCookies
			)
			;
		builder
			.Services
			.AddIdentity<IAuthenticatedUser, UserRole>()
			.AddRoles<UserRole>()
			.AddRoleStore<RoleStore>()
			.AddUserStore<UserStore>()
			;
		return builder;
	}
}
