using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallSignalTrigger : MonoBehaviour
{
    public string tagName;
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagName))
        {
            triggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            triggerExitEvent?.Invoke();
        }
    }
}
