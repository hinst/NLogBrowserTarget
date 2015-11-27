using System;
using System.Xml.Linq;
using NLog;

namespace NLogBrowserTarget
{

	public class LogEventInfoXmlWriter
	{
	
		public XElement toXElement(LogEventInfo logEventInfo)
		{
			var time = logEventInfo.TimeStamp;
			var timeString = time.ToString("yyyy.dd.MM-hh:mm:ss.zzzz");
			return new XElement("logMessage", 
				new XAttribute("time", timeString)
			);
		}
		
		public XElement toXElement(LogEventInfo[] logEventInfo)
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
