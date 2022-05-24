using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction curentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (curentAction == action) return;
            if(curentAction != null)
            {
                curentAction.Cancel();

            }
            curentAction = (IAction)action;

        }
    }
}
