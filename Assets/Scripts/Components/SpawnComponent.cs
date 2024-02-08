using Unity.Mathematics;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;

    public void Spawn() => Instantiate(_prefab, _spawnPoint.position, quaternion.identity);
    

}
