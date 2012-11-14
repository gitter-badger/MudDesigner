using System.Collections.Generic;
using log4net.Appender;
using log4net.Core;

namespace MudDesigner.Engine.Core
{
    public class MenuAppender : AppenderSkeleton
    {
        public static List<string> MessageCache = new List<string>();
        protected override void Append(LoggingEvent loggingEvent)
        {

            MessageCache.Add(string.Format("{0} - [{1}]: {2}",loggingEvent.TimeStamp, loggingEvent.Level.Name,loggingEvent.RenderedMessage));
        }
    }
}