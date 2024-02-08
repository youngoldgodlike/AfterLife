using System;
using Ui;
using UnityEngine;
using UnityEngine.Events;

namespace Minions
{
    public class MinionSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _minionPrefab;
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private UnityEvent OnSpawn = new();

        [ContextMenu("Spawn")]
        public GameObject SpawnGO()
        {
            var minion = Instantiate(_minionPrefab, _spawnPoint.position, Quaternion.identity);
            //OnSpawn.Invoke();
            return minion;
        }

        public void Spawn() {
            var minion = Instantiate(_minionPrefab, _spawnPoint.position, Quaternion.identity);
            OnSpawn.Invoke();
        }
    }

    public enum JobType
    {
        Lumberjack,
        Miner,
    }
}