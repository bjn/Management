using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NServiceBus.Management.Auditing.Core
{
    public interface IPersistAuditMessages
    {
        void Persist(AuditMessage message);
    }
}
