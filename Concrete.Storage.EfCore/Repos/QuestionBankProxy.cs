using Concrete.Core;

namespace Concrete.Storage.EfCore.Repos;

internal record QuestionBankProxy(Guid Id, IQuestionBank QuestionBank);
