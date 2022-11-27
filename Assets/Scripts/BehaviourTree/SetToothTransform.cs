using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using System.Linq;

namespace ATTT
{
    [MBTNode("ATTT/SetToothTransform")]
    [AddComponentMenu("")]
    public class SetToothTransform : Leaf
    {
        public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);
        private int index = 0;
        private int direction = 1;

        public override NodeResult Execute()
        {
            if (variableToSet.Value != null)
            {
                return NodeResult.success;
            }

            var teeth = GameObject.FindGameObjectsWithTag("Tooth");
            if (teeth.Length == 0)
            {
                return NodeResult.failure;
            }

            var results = teeth.Where((GameObject tooth) =>
            {
                return tooth.GetComponent<Tooth>().state == Tooth.State.VULNERABLE;
            }).OrderBy((GameObject tooth) =>
            {
                return Vector2.Distance(transform.position, tooth.transform.position); ;
            });

            if (results.Count() == 0)
            {
                return NodeResult.failure;
            }

            variableToSet.Value = results.First().transform;
            return NodeResult.success;
        }
    }
}
