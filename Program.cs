using System;
using NLog;

namespace NLogBrowserTarget
{

	class Program
	{
	
		Logger log = LogManager.GetCurrentClassLogger();
	
		public static void Main(string[] args)
		{
			var program = new Program();
			program.run();
		}
		
		void run()
		{
			
		}
		
	}
	
}