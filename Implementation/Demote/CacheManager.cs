/**
 * Copyright (C) moniüs, 2012.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@monius.net
 */

using System;
using System.Collections.Generic;

namespace monius.Cache
{
	/// <summary>
	/// 
	/// </summary>
	public partial class CacheManager<K, T>
		where K : class
		where T : class
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sign"></param>
		/// <param name="item"></param>
		/// <param name="expireAt"></param>
		public void AddOne(K sign, T item, TimeSpan expireAt)
		{
			//
			// locking should not be used because we use ConcurrentDictionary for thread safty,
			// but we need deterministic result,
			lock (_CachingSync)
			{
				//
				// calling clean is expensive so we call it here
				Clean();

				Cache.TryAdd(sign, new CachedObject<T>
				{
					CachedAt = DateTime.Now,
					ExpireAt = expireAt,
					Corn = item,
				});
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sign"></param>
		/// <param name="item"></param>
		public void AddOne(K sign, CachedObject<T> item)
		{
			AddOne(sign, item.Corn, item.ExpireAt);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sign"></param>
		public void Remove(K sign)
		{
			lock (_CachingSync)
			{
				//
				// calling clean is expensive so we call it here
				Clean();

				CachedObject<T> item;
				Cache.TryRemove(sign, out item);

				//
				// logging
				logger.Trace("CachedItemRemoved, {0}", item);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sign"></param>
		/// <returns></returns>
		public T GetOne(K sign)
		{
			lock (_CachingSync)
			{
				CachedObject<T> item;
				if (Cache.TryGetValue(sign, out item))
				{
					if (!item.IsOutdated())
						return item.Corn;
				}

				return null;
			}
		}

		/// <summary>
		/// <c>Shrink</c> must be called priodically to clean up the memory,
		/// </summary>
		public void Shrink()
		{
			lock (_CachingSync)
			{
				Clean();
			}
		}

		/// <summary>
		/// must be called in a sync lock,
		/// </summary>
		private static void Clean()
		{
			var list = new List<K>();

			foreach (var item in Cache)
			{
				var idol = item.Value;
				if (idol.IsOutdated())
				{
					list.Add(item.Key);
				}
			}

			foreach (var sign in list)
			{
				CachedObject<T> item;
				Cache.TryRemove(sign, out item);

				//
				// logging
				logger.Trace("CachedItemRemoved, {0}", item);
			}
		}
	}
}