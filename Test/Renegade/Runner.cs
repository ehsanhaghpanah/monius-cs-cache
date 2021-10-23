/**
 * Copyright (C) moniüs, 2012.
 * All rights reserved.
 * E. Haghpanah; haghpanah@monius.net
 */

using System;
using System.Threading;

namespace Renegade
{
	static public class Runner
	{
		/// <summary>
		/// 
		/// </summary>
		[STAThread]
		static public void Main()
		{
			try
			{
				var cs = new monius.Cache.CachedObject<string>("this")
				{
					ExpireAt = new TimeSpan(0, 0, 0, 4)
				};
				var cm = new monius.Cache.CacheManager<string, string>();
				cm.AddOne("test", cs);

				Thread.Sleep(2000);
				Console.WriteLine(cm.GetOne("test"));	// it will print out the item,

				Thread.Sleep(5000);
				Console.WriteLine(cm.GetOne("test"));	// it will print out nothing, as the cached object has been expired,
			}
			catch (Exception p)
			{
				Console.WriteLine(p);
			}
		}
	}
}