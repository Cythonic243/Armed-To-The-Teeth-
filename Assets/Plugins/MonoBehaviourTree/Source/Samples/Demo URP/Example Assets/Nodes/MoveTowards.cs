using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("Example/Move Towards")]
    [AddComponentMenu("")]
    public class MoveTowards : Leaf
    {
        public Vector3Reference targetPosition;
        public TransformReference transformToMove;
        public float speed = 0.1f;
        public float minDistance = 0f;

        public override NodeResult Execute()
        {
            Vector3 target = targetPosition.Value;
            Transform obj = transformToMove.Value;
            Animator animator = transform.gameObject.GetComponent<Animator>();
            // Move as long as distance is greater than min. distance
            float dist = Vector3.Distance(target, obj.position);
            if (dist > minDistance)
            {
                // Move towards target
                obj.position = Vector3.MoveTowards(
                    obj.position, 
                    target, 
                    (speed > dist)? dist : speed 
                );
                Vector3 direction = target - obj.position;
                direction.Normalize();
                if (animator != null)
                {
                    animator.SetFloat("ForwardX", direction.x);
                    animator.SetFloat("ForwardY", direction.y);
                }
                return NodeResult.running;
            }
            else
            {
                return NodeResult.success;
            }
        }
    }
}
