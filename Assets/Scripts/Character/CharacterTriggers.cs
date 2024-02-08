using Assets.Scripts.Ui;
using Ui;
using UnityEngine;

public class CharacterTriggers : MonoBehaviour
{
    [SerializeField] private DialogSystem _dialogSystem;
    
    private void OnTriggerStay(Collider other) {
        if(!Input.GetKeyDown(KeyCode.E)) return;

        if (other.TryGetComponent<IDialogVisitor>(out IDialogVisitor visit)) {
            if(!visit.IsOpenToDialog()) return;
            visit.EnterToDialogue(gameObject.transform, _dialogSystem);
        }
    }
    
}
