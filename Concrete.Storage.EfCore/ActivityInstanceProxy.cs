using Concrete.Core.Activities.Instances;

namespace Concrete.Storage.EfCore;

internal record ActivityInstanceProxy(Guid Id, IActivity Instance);
