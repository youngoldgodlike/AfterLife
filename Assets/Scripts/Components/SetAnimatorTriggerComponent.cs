using UnityEngine;

public class SetAnimatorTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private Animator _animator;

    public void SetAnimatorTrigger()
    {
        _animator.SetTrigger(_tag);
    }

}
