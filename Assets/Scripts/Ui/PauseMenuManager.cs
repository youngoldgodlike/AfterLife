using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _settingsPanel;
        private bool _isOpen = false;

        private void Start()
        {
            HashGameObject(_pauseMenu); 
            HashGameObject(_settingsPanel);  
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OpenMenu();
        }

        public void ResumeButton() => OpenMenu();
        
        public void BackSceneButton() => SceneManager.LoadScene("MainMenu");

        public void UseSettingsButton()
        {
            _pauseMenu.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void CloseSettingsButton()
        {
            _settingsPanel.SetActive(false);
            _pauseMenu.SetActive(true);
        }

        private void OpenMenu()
        {
            if (!_isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
                _isOpen = true;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                _settingsPanel.SetActive(false);
                _pauseMenu.SetActive(false);
                _isOpen = false;
            }
        }

        private void HashGameObject(GameObject go)
        {
            go.SetActive(true);
            go.SetActive(false);
        }
    }
}
