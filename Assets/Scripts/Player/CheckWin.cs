using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public int ballsStatus;
    public int checkBallStatus;

    [SerializeField] private PlayerController[] playerController;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject retryLevel;
    
    
    public void CheckStatus()
    {
        
        if (ballsStatus == 2 && playerController[0].moves == playerController[1].moves)
        {
            ui.SetActive(true);
            retryLevel.SetActive(false);
            foreach (PlayerController controller in playerController)
                controller.enabled = false;
        }
        else if (ballsStatus == 2 && playerController[0].moves != playerController[1].moves)
            retryLevel.SetActive(true);
    }
}
