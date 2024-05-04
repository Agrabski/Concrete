using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.Extensions.Quizes.Questions;
public class QuestionCategory
{
	public QuestionCategory? Parent { get; set; }
	public List<QuestionCategory> Children { get; set; } = [];
	public string Name { get; set; } = string.Empty;
}
