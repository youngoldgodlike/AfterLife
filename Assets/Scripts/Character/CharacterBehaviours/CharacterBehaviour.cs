using Assets.Scripts.Character.CharacterBehaviours;
using Assets.Scripts.Utils;
using Enemy.ZombieBehaviors.States;
using UnityEngine;
using Utils;

namespace Character.CharacterBehaviours
{
    public class CharacterBehaviour : Singleton<CharacterBehaviour>
    {
        [SerializeField] private bool _isFirstView;

        private ICharacterBehaviour _iCharacterBehaviour;

        private void Start() {
            if(GameEvents.hasInstance)
                GameEvents.Instance.OnReloadLevel += () => SetBehaviour(new Unleashed());
            
            if (_isFirstView) {
                SetBehaviour(new FirstPersonBehaviour());
            }
            else {
                SetBehaviour(new TopViewBehaviour());
            }
        }

        public void SetBehaviour(ICharacterBehaviour iCharacterBehaviour)
        {
            if (_iCharacterBehaviour == iCharacterBehaviour) return;

            _iCharacterBehaviour = iCharacterBehaviour;
            _iCharacterBehaviour.Set(transform);
        }

        [ContextMenu("SetFirstView")]
        public void SetFirstView()
        {
            SetBehaviour(new FirstPersonBehaviour());
        }
        
    }

    
}
