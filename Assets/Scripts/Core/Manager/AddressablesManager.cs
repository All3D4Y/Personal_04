using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : Singleton<AddressablesManager>
{
    Dictionary<string, object> cache = new();

    public async Task<T> LoadAsync<T>(AssetReferenceT<T> reference) where T : UnityEngine.Object
    {
        string key = reference.RuntimeKey.ToString();

        if (cache.ContainsKey(key))
            return (T)cache[key];

        var handle = reference.LoadAssetAsync();
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cache[key] = handle.Result;
            return handle.Result;
        }

        Debug.LogError($"Addressables Load 실패 : {key}");
        return null;
    }

    public void Release<T>(AssetReferenceT<T> reference) where T : UnityEngine.Object
    {
        string key = reference.RuntimeKey.ToString();
        if (cache.ContainsKey(key))
        {
            Addressables.Release(cache[key]);
            cache.Remove(key);
        }
    }
}
    
