using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "ParallelNode", order = 300)]
    public class ParallelNode : Leaf
    {
        List<MonoBehaviourTree> trees = new List<MonoBehaviourTree>();
        public MonoBehaviourTree tree1, tree2, tree3, tree4;
        bool isEnter = false;
        public override void OnEnter()
        {
            base.OnEnter();
            trees.Clear();
            if (tree1 != null) trees.Add(tree1);
            if (tree2 != null) trees.Add(tree2);
            if (tree3 != null) trees.Add(tree3);
            if (tree4 != null) trees.Add(tree4);
            foreach (var tree in trees)
            {
                tree.Restart();
            }
            isEnter = true;
        }

        public override NodeResult Execute()
        {
            if (trees.Count == 0)
            {
                return NodeResult.failure;
            }
            int successCount = 0;
            foreach (var tree in trees)
            {
                Node root = tree.GetRoot();
                if (root.status == Status.Failure)
                {
                    foreach (var t in trees)
                    {
                        t.GetRoot().OnExit();
                    }
                    return NodeResult.failure;
                }
                else if (root.status == Status.Success)
                {
                    successCount++;
                }
            }
            if (successCount == trees.Count)
            {
                return NodeResult.success;
            }
            return NodeResult.running;
        }

        public override bool IsValid()
        {
            return tree1 != null || tree2 != null || tree3 != null || tree4 != null;
        }

        public override void OnExit()
        {
            base.OnExit();
            isEnter = false;
        }

        private void Update()
        {
            if (!isEnter) return;
            foreach (var tree in trees)
            {
                tree.Tick();
            }
        }
    }
}
