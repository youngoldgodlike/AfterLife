using Minions;
using Minions.WorkerMinion;
using Ui;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Assets.Scripts
{
    public class Obstruction : MonoBehaviour
    {
        [SerializeField] private TimerProgress _progress;
        [SerializeField] private MinionSpawner _axeSpawner, _pickaxeSpawner;
        [SerializeField] private WorkStone _pickaxeWorkPlace;
        [SerializeField] private WorkTree _axeWorkPlace;
        [SerializeField, Min(0f)] private float _destructionTime;

        [SerializeField] private UnityEvent OnStartDestruction = new();
        [SerializeField] private UnityEvent OnFinishDestruction = new();

        private CutsceneMinion _axeWorker, _pickaxeWorker;
        private readonly Timer _timer = new();

        private void OnEnable() => _timer.OnTimerEnd += DestroyObstruct;
        private void OnDisable() => _timer.OnTimerEnd -= DestroyObstruct;

        [ContextMenu("НАЧАТЬ СТРОИТЕЛЬНЫЕ РАБОТЫ!!!")]
        public void StartDestruction() {
            _progress.SetTimer(_timer);
            _timer.StartTimer(_destructionTime);
            OnStartDestruction.Invoke();
        }

        public void SpawnWorkers() {
            _axeWorker = _axeSpawner.SpawnGO().GetComponent<CutsceneMinion>();
            _pickaxeWorker = _pickaxeSpawner.SpawnGO().GetComponent<CutsceneMinion>();
            Invoke(nameof(SetWork),1f);
        }

        public void SetWork() {
            _axeWorker.GetToWork(_axeWorkPlace);
            _pickaxeWorker.GetToWork(_pickaxeWorkPlace);
            
            OnFinishDestruction.AddListener(_axeWorker.WorkDone);
            OnFinishDestruction.AddListener(_pickaxeWorker.WorkDone);
        }

        private void DestroyObstruct() {
            OnFinishDestruction.Invoke();
            Destroy(gameObject);
        }
    }
}