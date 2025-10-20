using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Grpc.Client.Base
{
    public class CacheMetaData<TData>
    {
        public TData Data { get; }
        private DateTime CreateTime { get; }
        private TimeSpan FreshTime { get; }

        public CacheMetaData(TData data, double freshHours)
        {
            Data = data;
            CreateTime = DateTime.UtcNow;
            FreshTime = TimeSpan.FromHours(freshHours);
        }

        public CacheMetaData(TData data)
        {
            Data = data;
            CreateTime = DateTime.UtcNow;
            FreshTime = TimeSpan.FromHours(CacheSetting.DefaultFreshHours);
        }


        /// <summary>
        /// true=>过期 false=>没过期
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            return DateTime.UtcNow - CreateTime > FreshTime;
        }

        public TimeSpan TimeUntilExpiry => FreshTime - (DateTime.UtcNow - CreateTime);
    }

    /// <summary>
    /// 缓存控制器
    /// </summary>
    /// <typeparam name="TKey">记录类型或者实现了<see cref="IEquatable{T}"/>的类型>，以确保键的相等性判断</typeparam>
    /// <typeparam name="TData"></typeparam>
    public class CacheController<TKey, TData>
        where TKey : class, IEquatable<TKey>
        where TData : class
    {
        private readonly ConcurrentDictionary<TKey, CacheMetaData<TData>> _cache = new();

        /// <summary>
        /// 读取时，查询到未过期缓存返回true，过期缓存或无缓存返回false 并移除过期缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetCachedValue(TKey key, [MaybeNullWhen(false)] out TData value)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_cache.TryGetValue(key, out var cacheEntry))
            {
                value = null;
                return false;
            }

            //获取到缓存
            if (!cacheEntry.IsExpired())
            {
                value = cacheEntry.Data;
                return true;
            }

            _cache.TryRemove(key, out _);
            value = null;
            return false;
        }

        /// <summary>
        ///  向缓存中添加项目
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="freshHours">freshTime新鲜时间，单位为小时，默认1</param>
        public void AddOrUpdate(TKey key, TData value, double freshHours = CacheSetting.DefaultFreshHours)
        {
            ArgumentNullException.ThrowIfNull(key);
            var cacheMetaData = new CacheMetaData<TData>(value, freshHours);
            _cache.AddOrUpdate(key, cacheMetaData, (_, _) => cacheMetaData);
        }
    }

    /// <summary>
    /// 适用于单个成员的CacheController
    /// </summary>
    public class CacheController<TData>
        where TData : class
    {
        private CacheMetaData<TData>? _data;
        private readonly Lock _dataLock = new();

        public bool TryGetCachedValue([MaybeNullWhen(false)] out TData value)
        {
            lock (_dataLock)
            {
                if (_data == null)
                {
                    value = null;
                    return false;
                }

                if (_data.IsExpired())
                {
                    _data = null;
                    value = null;
                    return false;
                }

                value = _data.Data;
                return true;
            }
        }

        /// <summary>
        ///  向缓存中添加项目
        /// </summary>
        /// <param name="value"></param>
        /// <param name="freshHours">freshTime新鲜时间，单位为小时，默认1</param>
        public void AddOrUpdate(TData value, double freshHours=CacheSetting.DefaultFreshHours)
        {
            lock (_dataLock)
            {
                _data = new CacheMetaData<TData>(value, freshHours);
            }
        }
    }


    public static class CacheSetting
    {
        /// <summary>
        /// 默认所有元素5小时过期
        /// </summary>
        public const double DefaultFreshHours = 1;
    }
}