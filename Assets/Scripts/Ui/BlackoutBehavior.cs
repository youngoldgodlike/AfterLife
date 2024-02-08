using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class BlackoutBehavior : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private Animator _animator;
        
        public void SetScene()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(_tag);
        }

        public void StartBlackout() => _animator.SetTrigger("isClose");

    }
}
