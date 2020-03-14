using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DodgeHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        private void Awake()
        {
            if (CombatHandler.Unit.IsUnitPlayer)
            {
                CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(MakePlayerUnableToDodge);
                CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(MakePlayerAbleToDodgeAgain);
            }
        }

        public async void Dodge()
        {
            if(CombatHandler.UnitAnimator.Params.IsInDodgingState == true)
            {
                return;
            }

            var animationDodgeData = CombatHandler.UnitAnimator.AnimStatus.GetAnimationDodgeData();

            if(CombatHandler.UnitAnimator.Params.IsInDodgingState == false)
            {
                CombatHandler.UnitAnimator.OnDodgeAnimationStarted.Invoke();
            }

            if(KeyboardController.IsWASDPressed(out bool[] output))
            {
                if(output[(int)Direction.Right])
                {
                    transform.rotation = Quaternion.LookRotation(-transform.right, Vector3.up);
                }
                else if(output[(int)Direction.Left])
                {
                    transform.rotation = Quaternion.LookRotation(transform.right, Vector3.up);
                }
            }

            CombatHandler.UnitAnimator.Params.AnimDodge = animationDodgeData.paramValue;

            await AsyncManager.WaitForFrame(1);

            CombatHandler.UnitAnimator.Play(animationDodgeData.stateName);
        }

        public void SetDodgeAnimationStatus<Enum>(Enum stateValue, IReadOnlyDictionary<Enum, bool> statusDic)
            where Enum : System.Enum
        {
            CombatHandler.UnitAnimator.AnimStatus.SetToFalse(stateValue);

            bool isDodging = false;
            foreach (var dodge in statusDic)
            {
                if (dodge.Value == true)
                {
                    isDodging = true;
                    break;
                }
            }

            if (isDodging == false)
            {
                CombatHandler.UnitAnimator.OnDodgeAnimationFinished.Invoke();
                CombatHandler.UnitAnimator.Params.AnimDodge = 0;
            }
        }

        private void MakePlayerUnableToDodge()
        {
            if (CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_Dodge.RemoveListener(Dodge);
        }

        private void MakePlayerAbleToDodgeAgain()
        {
            if (CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_Dodge.AddListener(Dodge);
        }
    }
}
