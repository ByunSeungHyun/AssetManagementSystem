using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArcheFx;
using ArcheFx.Diagnostics;
using ArcheFx.Data;
using ArcheFx.EnterpriseServices;

namespace SystemAsset.Domain.Framework
{
	[
	SupportConnectionContext,
	SupportTransactionContext,
	TraceCategory("ArcheFx.EnterpriseServices"),
	ComponentType(ComponentType.BusinessLogic)
	]
#if DEBUG
    [SupportTraceCall]
#endif
	public class BizBase : ContextBoundComponent
	{
	}
}
