//-----------------------------------------------------------------------
// <copyright file="MenuAppender.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using log4net.Appender;
using log4net.Core;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Log4Net appender.
    /// </summary>
    public class MenuAppender : AppenderSkeleton
    {
        /// <summary>
        /// The message cache
        /// </summary>
        public static List<string> MessageCache = new List<string>();

        /// <summary>
        /// Subclasses of 
        /// <see cref="T:log4net.Appender.AppenderSkeleton" /> should implement this method 
        ///             to perform actual logging.
        /// </summary>
        /// <param name="loggingEvent">The event to append.</param>
        /// <remarks>
        /// <para>
        ///             A subclass must implement this method to perform
        ///             logging of the <paramref name="loggingEvent" />.
        ///             </para>
        /// <para>This method will be called by <see cref="M:log4net.Appender.AppenderSkeleton.DoAppend(log4net.Core.LoggingEvent)" />
        ///             if all the conditions listed for that method are met.
        ///             </para>
        /// <para>
        ///             To restrict the logging of events in the appender
        ///             override the <see cref="M:log4net.Appender.AppenderSkeleton.PreAppendCheck" /> method.
        ///             </para>
        /// </remarks>
        protected override void Append(LoggingEvent loggingEvent)
        {
            MessageCache.Add(string.Format("{0} - [{1}]: {2}",loggingEvent.TimeStamp, loggingEvent.Level.Name,loggingEvent.RenderedMessage));
        }
    }
}