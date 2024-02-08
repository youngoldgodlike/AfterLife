using UnityEngine;
using Utils;

namespace Assets.Scripts.Utils
{
    public class CheckPointManager : Singleton<CheckPointManager>
    {
        [SerializeField] private Transform _checkPoint;
        public Transform CheckPoint => _checkPoint;

        public void SetCheckPoint(Transform checkPoint) {
            _checkPoint = checkPoint;
        }
    }
}