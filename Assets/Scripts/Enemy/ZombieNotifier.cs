using System;
using UnityEngine;
using Utils;

public class ZombieNotifier : Singleton<ZombieNotifier>
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _zombiesScream;

    public event Action OnDiamondPickedUp;
    public event Action OnDialogStart;
    public event Action OnDialogEnd;

    public void DiamondPickedUp() {
        OnDiamondPickedUp?.Invoke();
        _audio.PlayOneShot(_zombiesScream);
    }

    public void DialogStart() => OnDialogStart?.Invoke();
    public void DialogEnd() => OnDialogEnd?.Invoke();

}
