using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Ui
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private string _startLevelTag;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private AudioMixer _audioMixer;

        private void Awake()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Start()
        {
            var value = Mathf.Lerp(-40,0f, PlayerPrefs.GetFloat("MasterVolume", 0.5f));
            _audioMixer.SetFloat("Master", value);
            
            var value1 = Mathf.Lerp(-40,0f, PlayerPrefs.GetFloat("MusicVolume", 0.5f));
            _audioMixer.SetFloat("Music", value1);
            
            var value2 = Mathf.Lerp(-40,0f, PlayerPrefs.GetFloat("SoundsVolume", 0.5f));
            _audioMixer.SetFloat("Sounds", value2);
        }
        
        public void StartLevel()
        {
            Time.timeScale = 1;
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void OpenSettings()
        {
            _mainPanel.SetActive(false);
            _settings.SetActive(true);
        }

        public void CloseSettings()
        {
            _settings.SetActive(false);
            _mainPanel.SetActive(true);
        }

    }
}
