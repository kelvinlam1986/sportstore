using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SpyStore.MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).UseUrls("http://*:40002/").Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
