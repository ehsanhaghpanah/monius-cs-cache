/**
 * Copyright (C) moniüs, 2012.
 * All rights reserved.
 * Ehsan Haghpanah; haghpanah@monius.net
 */

using System;

namespace monius.Cache
{
	/// <summary>
	/// generic cached object of type T,
	/// </summary>
	public class CachedObject<T> 
		where T : class 
	{
		/// <summary>
		/// 
		/// </summary>
		public CachedObject()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="corn"></param>
		public CachedObject(T corn)
		{
			Corn = corn;
		}

		/// <summary>
		/// 
		/// </summary>
		public T Corn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime CachedAt { get; set; }

		/// <summary>
		/// a <c>TimeSpan</c> in which the cached item must be flagged as expired,
		/// and hence it must be removed from the cache,
		/// </summary>
		public TimeSpan ExpireAt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool IsOutdated()
		{
			return (DateTime.Now.Subtract(CachedAt).CompareTo(ExpireAt) > 0);
		}
	}
}