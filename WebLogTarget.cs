using System;
using System.Net;
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
		const int defaultPort = 16109;
		readonly Queue<LogEventInfo> logEventQueue = new Queue<LogEventInfo>();
		HttpListener listener;
		
		public WebLogTarget()
		{
			port = defaultPort;
		}
		
		protected override void Write(LogEventInfo logEvent)
		{
			logEventQueue.Enqueue(logEvent);
			if (listener == null)
			{
				listener = new HttpListener();
				listener.Prefixes.Add("http://127.0.0.1:" + port + "/");
				listener.Start();
				listener.BeginGetContext(processRequest, null);
			}
		}
		
		void processRequest(IAsyncResult data)
		{
			var context = listener.EndGetContext(data);
			var request = context.Request;
			var response = context.Response;
		}
	
	}
	
}
