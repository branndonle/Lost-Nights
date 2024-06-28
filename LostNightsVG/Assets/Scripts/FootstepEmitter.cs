using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootstepEmitter : MonoBehaviour
{
    public void ExecuteFootstep()
    {
        // Play footstep sound
        EventManager.TriggerEvent<FootstepEvent, Vector3>(transform.position);
        //adfad
    }
}
