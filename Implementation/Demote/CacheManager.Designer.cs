/**
 * Copyright (C) moniüs, 2012.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@monius.net
 */

using System;
using System.Collections.Concurrent;

namespace monius.Cache
{
	/// <summary>
	/// 
	/// </summary>
	partial class CacheManager<K, T>
	{
		protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		#region —— Singleton ——

		protected static volatile ConcurrentDictionary<K, CachedObject<T>> _CachedItems;
		protected static readonly object _CachingSync = new object();

		/// <summary>
		/// readonly cache to accelerate reading,
		/// </summary>
		protected static ConcurrentDictionary<K, CachedObject<T>> Cache
		{
			get
			{
				//
				// since _CachedItems is volatile, 
				// it is safe to check null equality here,
				if (_CachedItems == null)
				{
					lock (_CachingSync)
					{
						_CachedItems = new ConcurrentDictionary<K, CachedObject<T>>();
					}
				}

				return _CachedItems;
			}
		}

		#endregion
	}
}