using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Ui;
using Cysharp.Threading.Tasks;
using Ui;
using UnityEngine;
using UnityEngine.Events;

public class MinionNotifier : MonoBehaviour
{
    [SerializeField] private UnityEvent OnAllMinionsSpawned = new();
    [SerializeField] private int _requiredMinions;
    private int _spawnedMinions;
    
    public void MinionSpawned() {
        _spawnedMinions++;

        CheckForComplete().Forget();
    }

    private async UniTask CheckForComplete() {
        await UniTask.WaitForSeconds(1f);
        
        if (_spawnedMinions == _requiredMinions) {
            await UniTask.WaitUntil(() => DialogSystem.Instance.IsActiveDialog == false);
            
            OnAllMinionsSpawned.Invoke();
        }
    }
}
