using Assets.Scripts.Utils;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static Cysharp.Threading.Tasks.UniTask;

namespace Ui
{
    public class ScreenEffects : MonoBehaviour
    {
        [SerializeField] private Image _screenObstacle;
        [SerializeField] private TextMeshProUGUI _loaderText;

        private void Start() {
            GameEvents.Instance.OnPlayerKilled += CloseScreen;
        }

        public void CloseScreen() {
            CloseScreenOnDeath().Forget();
        }

        public async UniTask CloseScreenAsync() {
            _screenObstacle.fillOrigin = (int)Image.OriginHorizontal.Left;
            _screenObstacle.fillAmount = 0f;
            
            while (_screenObstacle.fillAmount < 1f) {
                _screenObstacle.fillAmount += Time.deltaTime;

                await Yield();
            }

            _loaderText.enabled = true;
        }

        private async UniTask CloseScreenOnDeath() {
            _screenObstacle.fillOrigin = (int)Image.OriginHorizontal.Left;
            _screenObstacle.fillAmount = 0f;
            while (_screenObstacle.fillAmount < 1f) {
                _screenObstacle.fillAmount += Time.deltaTime;

                await Yield();
            }

            _loaderText.enabled = true;
            await WaitUntil(() => GameEvents.Instance.ReloadLevel());
            await WaitForSeconds(1f);
            _loaderText.enabled = false;
        
            _screenObstacle.fillOrigin = (int)Image.OriginHorizontal.Right;
            while (_screenObstacle.fillAmount > 0) {
                _screenObstacle.fillAmount -= Time.deltaTime;
            
                await Yield();
            }
        }
    }
}
