using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus.Config;
using NServiceBus.Management.Auditing.Core;
using Raven.Client.Document;

namespace NServiceBus.Management.Auditing.Persisters.Raven
{
    class RegisterRavenPersister : INeedInitialization
    {
        public void Init()
        {
            var persister = PersistAuditMessagesInRaven.Instance;
            DocumentStore documentStore = new DocumentStore { ConnectionStringName = "AuditMessagesDatabase" };
            documentStore.Initialize();
            persister.DocumentStore = documentStore;

            Configure.Instance.Configurer.RegisterSingleton<IPersistAuditMessages>(persister);

        }
    }
}
