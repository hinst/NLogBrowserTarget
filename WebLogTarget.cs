using System;
using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLogBrowserTarget
{

	[Target("WebBrowser")]
	public class WebLogTarget : Target
	{
	
		[DefaultParameter]
		public int port { get; set; }
		readonly Queue<LogEventInfo> logEventQueue = new Queue<LogEventInfo>();
		
		protected override void Write(LogEventInfo logEvent)
		{
		}
	
	}
}
