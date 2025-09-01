﻿namespace Dotnet.Sign.Infra.External
{
    public interface IDistributedMemoryCacheDAO
    {
        void SetValue(string key, string value, TimeSpan expiration);
        bool TryGetValue<T>(string key, out T? value);
        void DeleteValue(string key);
    }
}
