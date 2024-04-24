﻿using Concrete.Interface;

namespace Concrete.Core.Template;

public interface IActivityTemplate<TData>
{
	public ActivityName TypeName { get; }
	public Guid Id { get; init; } 
	public LocalisedText DisplayName { get; set; }
	public TData TemplateData { get; set; }
}
