using System;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private Transform newPos;

    private Vector3 oldPosition;
    
    private void Start()
    {
        oldPosition = gate.transform.position;
        ButtonActionHandler.OnButtonPressed += ButtonPressed;
        ButtonActionHandler.OnButtonLeft += ButtonLeft;
    }

    private void ButtonPressed(Transform other, ButtonController otherController)
    {
        LeanTween.move(otherController.gate, otherController.newPos.position, 10f * Time.deltaTime);
        other.GetComponent<MeshRenderer>().enabled = false;
    }

    private void ButtonLeft(Transform other, ButtonController otherController)
    {
        LeanTween.move(otherController.gate, otherController.oldPosition, 10f * Time.deltaTime);
        other.GetComponent<MeshRenderer>().enabled = true;
    }
}
