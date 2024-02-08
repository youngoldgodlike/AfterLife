using Assets.Scripts.Ui;
using Cysharp.Threading.Tasks;
using Minions.CommonStates;
using Minions.FighterStates;
using Ui;
using Unity.VisualScripting;
using UnityEngine;

namespace Minions.FighterMinion
{
    public class MinionFighter : Minion<EMeleeSoldierState>
    {
        [Header("Sword Minion")]
        [SerializeField] private TargetSearcher _searchTarget;
        [SerializeField] private Animator _swordAnimator;
        public Vector3 player => Interlocutor.position;

        private new void Start() {
            base.Start();
            DefaultState = EMeleeSoldierState.FollowPlayer;
            SetAgentMoveParameters(Speed,AngularSpeed,Acceleration,StoppingDistance);

            States.Add(EMeleeSoldierState.Dialogue, new DialogState<EMeleeSoldierState>(EMeleeSoldierState.Dialogue, this, Dialog));
            States.Add(EMeleeSoldierState.FollowPlayer, new FollowPlayerState(EMeleeSoldierState.FollowPlayer, this, agent,_swordAnimator));
            States.Add(EMeleeSoldierState.FollowTarget, new FollowTargetState(EMeleeSoldierState.FollowTarget, this, agent, _searchTarget, _swordAnimator));

            _searchTarget.OnTargetDetected.AddListener(() => {
                TransitionToState(EMeleeSoldierState.FollowTarget);
            });

            // if (!_player.IsUnityNull()) {
            //     CurrentState = States[EMeleeSoldierState.FollowPlayer];
            //     CurrentState.EnterState();
            // }

        }

        public override void BeginningDialog(Transform Player, DialogSystem dialogSystem) {
            Debug.Log("Begin dialog");
        
            Dialog = dialogSystem;
            Interlocutor = Player;

            TransitionToState(EMeleeSoldierState.Dialogue);
        }

        public void GoTo(Transform pos) {
            StandStillAt(pos.position).Forget();
        }
        
        [ContextMenu("FollowPlayer")]
        public void FollowPlayer() {
            TransitionToState(EMeleeSoldierState.FollowPlayer);
        }
        
        private async UniTask StandStillAt(Vector3 place) {
            CurrentState?.ExitState();
            CurrentState = null;
            agent.SetDestination(place);
            agent.stoppingDistance = 0f;

            var position = transform.position;
            var oldPos = position;
            
            await UniTask.Yield();
            await UniTask.WaitUntil(() => agent.remainingDistance < 0.5f);

            if (!oldPos.Equals(position)) {
                var dir = oldPos - position;
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }
}