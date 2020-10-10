using UnityEngine;

class MatchUI : MonoBehaviour
{
    [SerializeField] GameObject MainUI;
    [SerializeField] GameObject Play;

    public static MatchUI Instance;

    void Awake()
    {
        Instance = this;
    }
    
    public void OnLeave()
    {
        Leave(true);
    }

    public void Leave(bool askconfirm = true)
    {
        if(askconfirm)
        {
            Popup.ShowConfirm("Leave the match", "Are you sure? Your progress will be lost", () =>
            {
                Play.SetActive(false);
                MainUI.SetActive(true);
            }, null);
        }
        else
        {
            Play.SetActive(false);
            MainUI.SetActive(true);
        }
    }
}
