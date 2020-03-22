using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player_AICatcher : MonoBehaviour
    {
        private Unit_Player unitPlayer;
        public Unit_Player UnitPlayer => this.GetAndAssignComponent(ref unitPlayer);

        private Coroutine coroutine;

        private void Start()
        {
            UI_Menu.OnBeforeOpen.AddListener(StopCatcherCoroutine);
            UI_Menu.OnAfterClose.AddListener(StartCatcherCoroutine);

            UI_Chat_Main.Instance.Dialogue.OnDialogueActivated.AddListener(StopCatcherCoroutine);
            UI_Chat_Main.Instance.Dialogue.OnDialogueDeactivated.AddListener(StartCatcherCoroutine);

            StartCatcherCoroutine();
        }

        private void StartCatcherCoroutine()
        {
            if(coroutine != null)
            {
                return;
            }

            coroutine = StartCoroutine(GetAIWithinRangeCoroutine());
        }

        private void StopCatcherCoroutine()
        {
            if(coroutine == null)
            {
                return;
            }

            StopCoroutine(coroutine);
            coroutine = null;
        }

        private IEnumerator GetAIWithinRangeCoroutine()
        {
            var fourTimesPerSeconds = new WaitForSeconds(0.25f);

            var hostileList = new List<Unit_AI>();
            var neurtralList = new List<Unit_AI>();

            while (true)
            {
                var radius = 25.0f;
                var layer = LayerMask.GetMask(LayerManager.LAYER_AI);

                var colliders = Physics.OverlapSphere(transform.position, radius, layer);
                foreach (var c in colliders)
                {
                    var ai = c.GetComponent<Unit_AI>();

                    if(ai.Behaviour == AIBehaviour.Hostile)
                    {
                        hostileList.Add(ai);
                    }

                    else if (ai.Behaviour == AIBehaviour.Neutral)
                    {
                        neurtralList.Add(ai);
                    }
                }

                UnitPlayer.EnemyCatcher.Evaluate(hostileList);
                UnitPlayer.NeutralCathcer.Evaluate(neurtralList);

                hostileList.Clear();
                neurtralList.Clear();   

                yield return fourTimesPerSeconds;
            }
        }
    }
}
