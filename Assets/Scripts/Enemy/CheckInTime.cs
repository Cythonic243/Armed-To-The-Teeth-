using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace ATTT
{
    [MBTNode("ATTT/Is In Time")]
    [AddComponentMenu("")]
    public class CheckInTime : Leaf
    {
        public FloatReference timeSecs = new FloatReference(VarRefMode.DisableConstant);
        private float timeToEnd = 0;

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override NodeResult Execute()
        {
            if (timeToEnd < 0)
            {
                timeToEnd = Time.time + timeSecs.Value;
            }
            if (timeToEnd <= Time.time)
            {
                timeToEnd = -1;
                return NodeResult.failure;
            }
            
            return NodeResult.success;
        }
    }
}
