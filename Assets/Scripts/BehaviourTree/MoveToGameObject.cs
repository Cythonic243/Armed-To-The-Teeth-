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
        public float minDistance = 0f;
        public float timeToMove = 0.2f;
        float moveTimer = 0;
        Animator animator;
        Pathfinding.AIDestinationSetter aIDestinationSetter;
        Pathfinding.AILerp aILerp;
        public override void OnEnter()
        {
            base.OnEnter();
            animator = transform.gameObject.GetComponent<Animator>();
            aIDestinationSetter = transform.gameObject.GetComponent<Pathfinding.AIDestinationSetter>();
            aILerp = transform.gameObject.GetComponent<Pathfinding.AILerp>();
            moveTimer = 0;
        }
        public override NodeResult Execute()
        {
            Vector3 target = goalTransform.Value.position;
            Transform obj = transformToMove.Value;
            // Move as long as distance is greater than min. distance
            float dist = Vector3.Distance(target, obj.position);
            if (dist > minDistance && moveTimer < timeToMove)
            {
                moveTimer += Time.deltaTime;
                aIDestinationSetter.enabled = true;
                aILerp.enabled = true;
                aIDestinationSetter.target = goalTransform.Value;
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
