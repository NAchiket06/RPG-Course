using UnityEngine;
using System.Collections;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;

      public void StartAction(MonoBehaviour action)
      {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                Debug.Log("Cancelling" + currentAction);
            }
            currentAction = action;
            
      }
    }

}