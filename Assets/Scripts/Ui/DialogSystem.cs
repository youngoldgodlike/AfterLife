using System;
using System.Collections;
using Assets.Scripts.Components.Audio;
using Assets.Scripts.Cutscene;
using Character.CharacterBehaviours;
using TMPro;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Ui
{
    public class DialogSystem : Singleton<DialogSystem>
    {
        public Action DialogEnd;
    
        [Header("DialogSystem")]
        [SerializeField] private float _speedText;

        [SerializeField] private TextMeshProUGUI _dialogText;
        [SerializeField] private GameObject _playerPanel;
        [SerializeField] private GameObject _wormPanel;
        [SerializeField] private GameObject _defaultPanel;

        [Header("Audio")]
        [SerializeField] private PlayRandomSoundsComponent _randomAudio;
        [SerializeField] private PlaySoundsComponent _soundsComponent;

        private DialogString[] _dialogStrings;
        private PanelTag _panelTag;
        private CutsceneManager _cutsceneManager;
        private bool _isActiveDialog;
        private int _index = 0;

        public bool IsActiveDialog => _isActiveDialog;

        private new void Awake()
        {
            base.Awake();
            
            gameObject.SetActive(false);
            _dialogText.text = string.Empty;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ScipTextClick();
        }

        public void StartDialog(DialogData dialogData)
        {
            if (dialogData == null) return;
        
            _soundsComponent.Play("StartDialog");
            _isActiveDialog = true;
            _dialogStrings = dialogData.DialogStrings; 
            gameObject.SetActive(true); 
            CharacterBehaviour.Instance.SetBehaviour(new DialogBehaviour()); 
            _index = 0; 
            ChoicePanel();
        
        }

        private void ChoicePanel()
        {
            switch (_dialogStrings[_index].Tag)
            {
                case PanelTag.Player:
                    PlayerTalk();
                    break;
            
                case PanelTag.Worm:
                    WormTalk();
                    break;
            
                default:
                    DefaultTalk();
                    return;
            }
        }

        private void ScipTextClick()
        {
            if (_dialogText == null) return;
        
            if (_dialogText.text == _dialogStrings[_index].Message) 
                NextLines();
            else 
            {
                StopAllCoroutines(); 
                _dialogText.text = _dialogStrings[_index].Message;
            }   
        
        }

        private void NextLines()
        {
            if (_index < _dialogStrings.Length - 1)
            {
                _index++;
                _dialogText.text = String.Empty;
                ChoicePanel();
            }
            else
                FinishDialog();
        }

        private void FinishDialog()
        {
            if (!CutsceneManager.Instance.IsActiveCutscene)
                CharacterBehaviour.Instance.SetBehaviour(new IdleBehaviour());
            
            _soundsComponent.Play("ExitDialog");
            _dialogText.text = String.Empty;
            gameObject.SetActive(false);

            _isActiveDialog = false;

            DialogEnd?.Invoke();
        }

        private IEnumerator TypeLine()
        {
            foreach (char c in _dialogStrings[_index].Message.ToCharArray())
            {
                _dialogText.text += c;
                _randomAudio.Play();
                yield return new WaitForSeconds(_speedText);
            }
        }

        private void PlayerTalk()
        {
            _defaultPanel.SetActive(false);
            _wormPanel.SetActive(false);
        
            _playerPanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
    
        private void WormTalk()
        {
            _defaultPanel.SetActive(false);
            _playerPanel.SetActive(false);
        
            _wormPanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
        
        private void DefaultTalk()
        {
            _wormPanel.SetActive(false);
            _playerPanel.SetActive(false);
        
            _defaultPanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
    
    }
}
