using Assets.Scripts.Resource;
using TMPro;
using UnityEngine;

public class GraveCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textWood;
    [SerializeField] private TextMeshProUGUI _textStone;
    private GraveBehavior _grave;

    private void Awake()
    {
        _grave = GetComponentInParent<GraveBehavior>();
            // ShowValueResources(0, 0);
    }

    /*private void OnEnable()
    {
        GraveBehavior.OnAddResource += ShowValueResources;
    }

    private void OnDisable()
    {
        GraveBehavior.OnAddResource -= ShowValueResources;
    }
    
    private void ShowValueResources(int currentSize, int maxSize)
    {
        _textWood.text = $"Woods: {_grave.NeedItems[id].Count}/{_grave.NeedItems}";
        _textStone.text = $"Stones: {_grave.CurrentStone}/{_grave.MaxNeedStone}";
    }*/
    
    

}
