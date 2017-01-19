﻿using System;
using System.Collections.Generic;

namespace Messaging
{
    /// <summary>Read only view of a message</summary>
    [System.ComponentModel.ImmutableObject(true)]
    public interface IReadOnlyMessage
    {
        /// <summary>The subject (topic) of this message.  Think about this as the subject of an e-mail.</summary>
        /// <remarks>This is the Label for MSMQ messages, the SendSubject of Tibco RV messages</remarks>
        string Subject { get; }

        /// <summary>Where to send a reply to</summary>
        /// <example>msmq://host/PRIVATE$/queue</example>
        /// <example>rv://service/topic/subtopic/etc</example>
        Uri ReplyTo { get; }

        /// <summary>System and user defined message attributes</summary>
        IReadOnlyMessageHeaders Headers { get; }

        object Body { get; }

        /// <summary>Manually acknowlege a message, ignored if messages are automatically acknowledged</summary>
        /// <remarks>Used to acknowlegde MSMQ transactional messages, and RV certified messages</remarks>
        void Acknowledge();
    }

    /// <summary>Read only view of message headers</summary>
    [System.ComponentModel.ImmutableObject(true)]
    public interface IReadOnlyMessageHeaders : IReadOnlyDictionary<string, object>
    {
        /// <summary>Sets an expiry time for this message</summary>
        TimeSpan? TimeToLive { get; }

        /// <summary>The MIME type of the message, <see cref="ContentTypes"/> for common settings</summary>
        string ContentType { get; }

        /// <summary>the priority of this message</summary>
        /// <example>1</example>
        /// <example>2</example>
        /// <remarks>Not supported by Tibco RV</remarks>
        int? Priority { get; }
    }

    public static class ContentTypes
    {
        public const string PlainText = "text/plain";
        public const string Xml = "text/xml";
        public const string Json = "application/json";
        public const string Binary = "application/octet-stream";
    }

}