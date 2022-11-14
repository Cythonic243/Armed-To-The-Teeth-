using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace ATTT
{
    [MBTNode("ATTT/Print")]
    [AddComponentMenu("")]
    public class Print : Leaf
    {
        public StringReference reference;
        
        public override NodeResult Execute()
        {
            Debug.Log(reference.Value);
            
            return NodeResult.success;
        }
    }
}
