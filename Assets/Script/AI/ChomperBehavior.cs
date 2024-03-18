using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        BTTask_Wait waitTask = new BTTask_Wait(2f);
        BTTask_Log log = new BTTask_Log("logging");

        Selector Root = new Selector();
        Root.AddChild(log);
        Root.AddChild(waitTask);

        rootNode = Root;
    }
}
