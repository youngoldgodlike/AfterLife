using System;
using System.Collections.Generic;
using Assets.Scripts.Ui;
using Character.CharacterBehaviours;
using Cysharp.Threading.Tasks;
using Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Utils;

namespace Assets.Scripts.Cutscene
{
    public class CutsceneManager : Singleton<CutsceneManager>
    {

        [SerializeField] private List<CutsceneStruct> cutscenes = new List<CutsceneStruct>();
        
        public  bool IsActiveCutscene { get; private set; }
        
        private Dictionary<string, PlayableDirector> cutsceneDataBase = new Dictionary<string, PlayableDirector>();
        
        private PlayableDirector activeCutscene;

        private void Awake()
        {
            base.Awake();

            InitializeCutsceneDataBase();

            foreach (var cutscene in cutsceneDataBase)
            {
                cutscene.Value.gameObject.SetActive(false);
            }
        }

        private void InitializeCutsceneDataBase()
        {
            cutsceneDataBase.Clear();

            for (int i = 0; i < cutscenes.Count; i++)
            {
                cutsceneDataBase.Add(cutscenes[i].cutsceneKey, cutscenes[i].cutsceneObject);
            }
        }

        public void PauseCutscene()
        {
            activeCutscene.Pause();
            ContinueCutscene().Forget();
        }

        private async UniTaskVoid ContinueCutscene()
        {
            await UniTask.WaitUntil(() => DialogSystem.Instance.IsActiveDialog == false);
            activeCutscene.Play();
        }

        public void StartCutscene(string cutsceneKey)
        {
            if (activeCutscene != null)
            {
                if (activeCutscene == cutsceneDataBase[cutsceneKey])
                    return;
            }

            activeCutscene = cutsceneDataBase[cutsceneKey];

            foreach (var cutscene in cutsceneDataBase)
            {
                cutscene.Value.gameObject.SetActive(false);
            }
            
            cutsceneDataBase[cutsceneKey].gameObject.SetActive(true);
            IsActiveCutscene = true;
            
            CharacterBehaviour.Instance.SetBehaviour(new DialogBehaviour());
            
        }

        public void EndCutscene()
        {
            if (activeCutscene == null) return;
            
            activeCutscene.gameObject.SetActive(false);
            IsActiveCutscene = false;
            activeCutscene = null;
            CharacterBehaviour.Instance.SetBehaviour(new IdleBehaviour());
        }


        [Serializable]
        public struct CutsceneStruct
        {
            public string cutsceneKey;
            public PlayableDirector cutsceneObject;
        }

    }
    
}
