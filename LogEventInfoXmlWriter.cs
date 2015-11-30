using System;
using System.Xml.Linq;
using NLog;

namespace NLogBrowserTarget
{

	public class LogEventInfoXmlWriter
	{
	
		public const string timeFormat = "yyyy.dd.MM-HH:mm:ss.fff";
	
		public XElement toXElement(LogEventInfoEx logEventInfo)
		{
			var time = logEventInfo.V.TimeStamp;
			var timeString = time.ToString(timeFormat);
			return new XElement("logMessage", 
				new XAttribute("time", timeString),
				new XAttribute("level", logEventInfo.V.Level),
				new XAttribute("name", logEventInfo.V.LoggerName),
				new XAttribute("message", logEventInfo.V.Message),
				new XAttribute("threadID", logEventInfo.threadID)
			);
		}
		
		public XElement toXElement(LogEventInfoEx[] logEventInfo)
		{
			var elements = new XElement[logEventInfo.Length];
			for (var i = 0; i < logEventInfo.Length; i++)
			{
				elements[i] = toXElement(logEventInfo[i]);
			}
			return new XElement("logMessages", elements);
		}
		
	}
	
}
