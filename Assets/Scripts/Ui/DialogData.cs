using System;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    [CreateAssetMenu(fileName = "DialogData", menuName = "Pressets/Dialogs")]

    public class DialogData : ScriptableObject
    {
        [SerializeField] private DialogString[] _dialogStrings;
        public DialogString[] DialogStrings => _dialogStrings;
    }

    [Serializable]
    public struct DialogString
    {
        [SerializeField] private PanelTag _tag;
        [SerializeField] private string _message;
    
        public PanelTag Tag => _tag;
        public string Message => _message;
    }

    public enum PanelTag
    {
        Player,
        Worm,
        Default
    }
}