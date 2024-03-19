using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeResult
{
    Success,
    Failure,
    Inprogress
}

public class BTNode
{
    bool started = false;
    int priority;
    public NodeResult UpdateNode()
    {
        //one off thing
        if(!started)
        {
            started = true;
            NodeResult execResult = Execute();
            if(execResult != NodeResult.Inprogress)
            {
                EndNode();
                return execResult;
            }
        }

        //time based
        NodeResult updateResult = Update();
        if(updateResult != NodeResult.Inprogress)
        {
            EndNode();
        }
        return updateResult;
    }

    //override in child class
    protected virtual NodeResult Execute()
    {
        return NodeResult.Success;
    }

    protected virtual NodeResult Update()
    {
        //time based
        return NodeResult.Success;
    }

    protected virtual void End()
    {
        //clean up
    }

    private void EndNode()
    {
        started = false;
        End();
    }
}
