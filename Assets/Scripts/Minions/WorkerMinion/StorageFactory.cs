using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Minions
{
    internal static class StorageFactory
    {
        public static async UniTask<MinionStorage> CreateStorage(Vector3 position) {
            var storage = await Resources.LoadAsync("Minions/Storage",typeof(GameObject)) as GameObject;
            var obj = Object.Instantiate(storage,position,Quaternion.identity);
            var scrpt = obj.GetComponent<MinionStorage>();

            return scrpt;
        }
    }
}