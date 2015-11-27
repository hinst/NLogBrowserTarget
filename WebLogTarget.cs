using System;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLogBrowserTarget
{

	[Target("WebLog")]
	public class WebLogTarget : Target
	{
	
		[DefaultParameter]
		public int port { get; set; }
		const int defaultPort = 16109;
		readonly Queue<LogEventInfo> logEventQueue = new Queue<LogEventInfo>();
		HttpListener listener;
		string logPage;
		
		public WebLogTarget()
		{
			port = defaultPort;
			listener = new HttpListener();
			listener.Prefixes.Add("http://127.0.0.1:" + port + "/page/");
			listener.Prefixes.Add("http://127.0.0.1:" + port + "/update/");
			listener.Start();
			listener.BeginGetContext(processRequest, null);
			prepareLogPage();
		}
		
		string readTextResource(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = assembly.GetName().Name + "." + name;
			Console.WriteLine("!!!"+resourceName+"!!!");
			var stream = assembly.GetManifestResourceStream(resourceName);
			var streamReader = new StreamReader(stream, Encoding.UTF8);
			var text = streamReader.ReadToEnd();
			return text;
		}
		
		void prepareLogPage()
		{
			logPage = readTextResource("logPage.html");
			logPage = logPage.Replace("$bootstrap-css$", readTextResource("bootstrap.min.css"));
		}
		
		protected override void Write(LogEventInfo logEvent)
		{
			logEventQueue.Enqueue(logEvent);
		}
		
		void processRequest(IAsyncResult data)
		{
			var context = listener.EndGetContext(data);
			var request = context.Request;
			var localPath = request.Url.LocalPath;
			var response = context.Response;
			var responseText = "";
			if (localPath == "/page/")
			{
				response.Headers["Content-Type"] = "text/html; encoding=utf-8";
				responseText = getPage();
			}
			if (localPath == "/update/")
			{
				response.Headers["Content-Type"] = "text/json; encoding=utf-8";
				responseText = getUpdate();
			}
			var responseData = Encoding.UTF8.GetBytes(responseText);
			response.OutputStream.Write(responseData, 0, responseData.Length);
			response.OutputStream.Close();
			listener.BeginGetContext(processRequest, null);
		}
		
		string getPage()
		{
			return logPage;
		}
		
		string getUpdate()
		{
		}
		
	}
	
}
