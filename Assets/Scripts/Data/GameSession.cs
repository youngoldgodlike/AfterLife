using Cysharp.Threading.Tasks;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Data
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private ScreenEffects _screenEffects;
        private void Awake()
        {
            LoadGameMenu();
        }

        private void LoadGameMenu()
        {
            SceneManager.LoadScene("GameMenu", LoadSceneMode.Additive);
        }

        public void LoadScene(string scene) {
            LoadSceneAsync(scene).Forget();
        }

        private async UniTask LoadSceneAsync(string scene) {
            await _screenEffects.CloseScreenAsync();
            SceneManager.LoadScene(scene);
            Debug.Log("LOL))");
        }
    }
}
