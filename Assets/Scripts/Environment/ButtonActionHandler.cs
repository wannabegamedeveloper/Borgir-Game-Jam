using System;
using UnityEngine;

public class ButtonActionHandler : MonoBehaviour
{
    public static event Action<Transform, ButtonController> OnButtonPressed;
    public static event Action<Transform, ButtonController> OnButtonLeft;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
            OnButtonPressed?.Invoke(other.transform, other.GetComponent<ButtonController>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
            OnButtonLeft?.Invoke(other.transform, other.GetComponent<ButtonController>());
    }
}
