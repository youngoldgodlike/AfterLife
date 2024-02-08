using System.Collections.Generic;
using Minions.WorkerMinion;
using Utils;

namespace Minions
{
    public class MinionManager : Singleton<MinionManager>
    {
        private new void Awake() {
            base.Awake();
            CollectWorkPlaces();
        }
        private Queue<WorkTree> _freeTrees;
        private Queue<WorkStone> _freeStones;

        private void CollectWorkPlaces()
        {
            var jobTrees = FindObjectsOfType<WorkTree>();
            var jobStones = FindObjectsOfType<WorkStone>();

            _freeTrees = new Queue<WorkTree>(jobTrees);
            _freeStones = new Queue<WorkStone>(jobStones);
        }

        public WorkPlace GetFreeJobPlace(JobType jobType) {
            return jobType switch {
                JobType.Lumberjack => _freeTrees.Dequeue(),
                JobType.Miner => _freeStones.Dequeue(),
                _ => null
            };
        }
    }
}