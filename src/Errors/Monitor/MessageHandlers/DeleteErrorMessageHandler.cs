﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus.Management.Errors.Messages;
using System.Configuration;

namespace NServiceBus.Management.Errors.Monitor.MessageHandlers
{
    class DeleteErrorMessageHandler : IHandleMessages<DeleteErrorMessage>
    {
        public IBus Bus { get; set; }
        public IPersistErrorMessages ErrorPersister { get; set; }

        private DeleteMessageFromQueueHelper deleteHelper = new DeleteMessageFromQueueHelper();

        public void Handle(DeleteErrorMessage message)
        {
            Console.WriteLine("Removing message {0}", message.OriginalMessageId);

            // Get rid of message in the storage queue
            deleteHelper.InputQueue = new Address(string.Format("{0}.Storage", ConfigurationManager.AppSettings["ErrorQueueToMonitor"]), Environment.MachineName);
            deleteHelper.DeleteMessageFromSourceQueue(message.OriginalMessageId);
            
            // Get rid of message in the persistent store.
            ErrorPersister.DeleteErrorMessage(message.OriginalMessageId);

            // Publish event
            Bus.Publish<ErrorMessageDeleted>(m => { m.MessageId = message.OriginalMessageId; m.ErrorDeletedTime = DateTime.Now; });
        }
    }
}
