using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace ATTT
{
    [MBTNode("ATTT/Move To GameObject")]
    [AddComponentMenu("")]
    public class MoveToGameObject : Leaf
    {
        public TransformReference goalTransform;
        public TransformReference transformToMove;
        public float speed = 0.1f;
        public float minDistance = 0f;
        Animator animator;
        Pathfinding.AIDestinationSetter aIDestinationSetter;
        Pathfinding.AILerp aILerp;
        public override void OnEnter()
        {
            base.OnEnter();
            animator = transform.gameObject.GetComponent<Animator>();
            aIDestinationSetter = transform.gameObject.GetComponent<Pathfinding.AIDestinationSetter>();
            aILerp = transform.gameObject.GetComponent<Pathfinding.AILerp>();
        }
        public override NodeResult Execute()
        {
            Vector3 target = goalTransform.Value.position;
            Transform obj = transformToMove.Value;
            // Move as long as distance is greater than min. distance
            float dist = Vector3.Distance(target, obj.position);
            if (dist > minDistance)
            {
                aIDestinationSetter.enabled = true;
                aILerp.enabled = true;
                aIDestinationSetter.target = goalTransform.Value;
                // Move towards target
                //obj.position = Vector3.MoveTowards(
                //    obj.position, 
                //    target, 
                //    (speed > dist)? dist : speed 
                //);
                //Vector3 direction = target - obj.position;
                //direction.Normalize();
                //if (animator != null)
                //{
                //    animator.SetFloat("ForwardX", direction.x);
                //    animator.SetFloat("ForwardY", direction.y);
                //}
                return NodeResult.running;
            }
            else
            {
                aIDestinationSetter.enabled = false;
                aILerp.enabled = false;
                aIDestinationSetter.target = null;
                return NodeResult.success;
            }
        }
    }
}
