using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player_NeutralCatcher : MonoBehaviour
    {
        private Unit_Player unitPlayer;
        public Unit_Player UnitPlayer => this.GetAndAssignComponent(ref unitPlayer);

        public void Evaluate(IReadOnlyCollection<Unit_AI> neutralList)
        {
            var playerForward = UnitPlayer.transform.forward;
            playerForward.y = 0;
            playerForward = Vector3.Normalize(playerForward);

            foreach(var i in neutralList)
            {
                var dirToPlayer = UnitPlayer.transform.position - i.transform.position;
                dirToPlayer.y = 0;
                dirToPlayer = Vector3.Normalize(dirToPlayer);
                var dot = Vector3.Dot(playerForward, dirToPlayer);

                if(dot > -0.9f)
                {
                    continue;
                }

                var distanceToPlayer = Vector3.Distance(UnitPlayer.transform.position, i.transform.position);
                if(distanceToPlayer > 3.0f)
                {
                    continue;
                }

                if (GameManager.IsAbleToInteract == false)
                {
                    return;
                }

                UI_Chat_Main.Instance.Bubble.Activate(i);
            }
        }
    }
}
