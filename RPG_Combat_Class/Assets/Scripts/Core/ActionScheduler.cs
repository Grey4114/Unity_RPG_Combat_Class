using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // This script determines when movement or combat occurs
    public class ActionScheduler : MonoBehaviour 
    {
        IAction currentAction;

        // Breakdown/Explanation for how this works
        // https://community.gamedev.tv/t/actionscheduler-help/159058
        // Video - https://www.gamedev.tv/courses/637539/lectures/11879231
        public void StartAction(IAction action)
        {
            if(currentAction == action) return;

            if (currentAction != null)
            {
                currentAction.Cancel();
                //print("Cancelling " + currentAction);
            }
            
            currentAction = action;
            
        }
    }
}