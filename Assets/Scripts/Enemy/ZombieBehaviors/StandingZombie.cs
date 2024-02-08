using System;
using Cysharp.Threading.Tasks;
using Enemy.ZombieBehaviors.States;
using UnityEngine;

namespace Enemy.ZombieBehaviors
{
    public class StandingZombie : Zombie
    {
        private new void Start() {
            base.Start();
            States.Add(ZombieState.Pursuit, new Pursuit(ZombieState.Pursuit, this, Agent, Animator));
            States.Add(ZombieState.Staying, new Staying(ZombieState.Staying, this, Agent, Animator));

            CurrentState = States[ZombieState.Staying];
            CurrentState.EnterState();
        }
    }
}