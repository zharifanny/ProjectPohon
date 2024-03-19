using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComp : MonoBehaviour
{
    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();
    List<PerceptionStimuli> PerceivableStimulis = new List<PerceptionStimuli>();

    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if(registeredStimulis.Contains(stimuli))
            return;
        
        registeredStimulis.Add(stimuli);
        // Debug.Log($"Stimuli {stimuli.gameObject} added.");
    }

    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    // Update is called once per frame
    void Update()
    {
        foreach(var stimuli in registeredStimulis)
        {
            if(IsStimuliSensable(stimuli))
            {
                if(!PerceivableStimulis.Contains(stimuli))
                {
                    Debug.Log($"I just sensed {stimuli.gameObject}");
                    PerceivableStimulis.Add(stimuli);
                }
            }
            else
            {
                if(PerceivableStimulis.Contains(stimuli))
                {
                    Debug.Log($"I lost track of {stimuli.gameObject}");
                    PerceivableStimulis.Remove(stimuli);
                }
            }
        }
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}