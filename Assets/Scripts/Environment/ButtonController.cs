using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private Transform newPos;
    
    public void ButtonPressed()
    {
        LeanTween.move(gate, newPos.position, 10f * Time.deltaTime);
    }
}
