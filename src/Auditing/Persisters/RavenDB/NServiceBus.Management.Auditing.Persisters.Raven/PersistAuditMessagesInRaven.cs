﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus.Management.Auditing.Core;
using Raven.Client;

namespace NServiceBus.Management.Auditing.Persisters.Raven
{
    public class PersistAuditMessagesInRaven : IPersistAuditMessages
    {
        private PersistAuditMessagesInRaven() { }

        private static readonly PersistAuditMessagesInRaven _instance = new PersistAuditMessagesInRaven();
        public static PersistAuditMessagesInRaven Instance { get { return _instance; } }

        public IDocumentStore DocumentStore { get; set; }

        public void Persist(AuditMessage message)
        {
            using (var session = DocumentStore.OpenSession())
            {
                string id = message.MessageId;
                session.Store(message);
                session.SaveChanges();
            }
        }
    }
}
