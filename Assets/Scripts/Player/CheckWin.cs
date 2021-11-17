using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public int ballsStatus;
    public int checkBallStatus;

    [SerializeField] private PlayerController[] playerController;
    [SerializeField] private GameObject ui;
    
    public void CheckStatus()
    {
        
        if (ballsStatus == 2 && playerController[0].moves == playerController[1].moves)
        {
            ui.SetActive(true);
            foreach (PlayerController controller in playerController)
                controller.enabled = false;
        }
    }
}
