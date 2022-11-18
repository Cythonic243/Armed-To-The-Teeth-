using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace ATTT
{
    [MBTNode("ATTT/Set Player Transform")]
    [AddComponentMenu("")]
    public class SetPlayerTransform : Leaf
    {
        public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);
        private int index = 0;
        private int direction = 1;

        public override NodeResult Execute()
        {
            var o = GameObject.FindGameObjectWithTag("Player");
            variableToSet.Value = o.transform;
            return NodeResult.success;
        }
    }
}
