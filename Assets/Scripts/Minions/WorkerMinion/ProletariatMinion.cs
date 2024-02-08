using Assets.Scripts.Ui;
using Character.CharacterBehaviours;
using Minions.CommonStates;
using Minions.ProletariatStates;
using Ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minions.WorkerMinion
{
    public class ProletariatMinion : Minion<EWorkMinionState>
    {
        [Header("Working Minion")] 
        [SerializeField] private WorkPlace _work;
        [SerializeField] private JobType _jobType;
        
        [Header("Parameters")]
        [SerializeField,Range(0f,20f)] private float _walkingAreaRange = 10f;
        
        private Vector3 position => transform.position;
        private Vector3 _stock;

        private new void Start() {
            base.Start();
            _stock = transform.position;
            SetAgentMoveParameters(Speed,AngularSpeed,Acceleration,StoppingDistance);
            AssignWork();

            States.Add(EWorkMinionState.Dialogue, new DialogState<EWorkMinionState>(EWorkMinionState.Dialogue, this, Dialog));
            States.Add(EWorkMinionState.Working, new WorkingState(EWorkMinionState.Working, this, agent, _work, GhostAnimator));
            States.Add(EWorkMinionState.Unemployed, new FreeState(EWorkMinionState.Unemployed, this, agent));

            CurrentState = States[EWorkMinionState.Dialogue];
            CurrentState.EnterState();
        }

        #region Minion Actions For States

        public Vector3 GetWalkingAreaPoint() {
            var range1 = Random.Range(-_walkingAreaRange, _walkingAreaRange);
            var range2 = Random.Range(-_walkingAreaRange, _walkingAreaRange);
            var stock = _stock += new Vector3(range1,
                0f,
                range2);

            return stock;
        }

        #endregion


        public override void BeginningDialog(Transform player,DialogSystem dialogSystem) {
            Debug.Log("Begin startoviy dialog");
        
            Dialog = dialogSystem;
            Interlocutor = player;

            TransitionToState(EWorkMinionState.Dialogue);
        }

        private void AssignWork() {
            _work = MinionManager.Instance.GetFreeJobPlace(_jobType);
            _work.SetOwner(gameObject); 
            _work.OnDestroyPlace.AddListener(() => {
                TransitionToState(EWorkMinionState.Unemployed);
            });
        }
    }
}