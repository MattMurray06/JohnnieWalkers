using UnityEngine;
using UnityEngine.UI;

public class NextLevelManager : MonoBehaviour
{
    public GameObject popUpPanel;
    public Button yesButton;
    public Button noButton;

    private System.Action<bool> callback; 

    void Start()
    {
        popUpPanel.SetActive(false);
    }

    public void ShowPopUp()
    {
        popUpPanel.SetActive(true);
    }
}
