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
    }

    public void ButtonPressed()
    {
        LeanTween.move(gate, newPos.position, 10f * Time.deltaTime);
    }

    public void ButtonLeft()
    {
        LeanTween.move(gate, oldPosition, 10f * Time.deltaTime);
    }
}
