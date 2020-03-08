using UnityEngine;

namespace Tamana
{
    public struct Environment_PlayerDistanceData
    {
        private float distanceToPlayer;
        private readonly Vector3 myPos;
        private readonly Vector3 playerPos;
        private readonly int objectInstanceID;

        public Environment_PlayerDistanceData(Vector3 myPos, Vector3 playerPos, int objectInstanceID)
        {
            this.myPos = myPos;
            this.playerPos = playerPos;
            this.objectInstanceID = objectInstanceID;
            distanceToPlayer = 0.0f;
        }

        public void Calculate()
        {
            distanceToPlayer = Vector3.Distance(myPos, playerPos);
 
            if (distanceToPlayer < 10)
            {
                Environment_Main.idWithDistanceBelow10.Enqueue(objectInstanceID);
            }
            else if(distanceToPlayer >= 10 && distanceToPlayer < 15)
            {
                Environment_Main.idWithDistanceBetween10and15.Enqueue(objectInstanceID);
            }
        }
    }
}
