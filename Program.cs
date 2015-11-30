using System;
using System.Threading;
using NLog;

namespace NLogBrowserTarget
{

	class Program
	{
	
		Logger log = LogManager.GetCurrentClassLogger();
		bool postSpamsTerminated;
		Thread postSpamsThread;
	
		public static void Main(string[] args)
		{
			var program = new Program();
			program.run();
		}
		
		void run()
		{
			log.Debug("heh");
			postSpamsThread = new Thread(postSpams);
			postSpamsThread.Start();
			while (true)
			{
				var command = Console.ReadLine();
				if (command == "e")
				{
					postSpamsTerminated = true;
					postSpamsThread.Join();
					break;
				}			
			}
		}
		
		void postSpams()
		{
			var counter = 0;
			while (false == postSpamsTerminated)
			{
				Thread.Sleep(600);
				log.Debug("heh " + counter + " " + Thread.CurrentThread.ManagedThreadId + " <b>");
				counter++;
			}		
		}
		
	}
	
}