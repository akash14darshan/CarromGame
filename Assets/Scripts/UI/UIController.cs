using UnityEngine;

class UIController : MonoBehaviour
{
    [SerializeField] GameObject HomeUI;

    [SerializeField] GameObject Play;
    [SerializeField] GameObject HowTo;

    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void OnPlay()
    {
        gameObject.SetActive(false);
        Play.SetActive(true);
    }

    public void OnHowTo()
    {
        HomeUI.SetActive(false);
        HowTo.SetActive(true);
    }

    public void OnHome()
    {
        HowTo.SetActive(false);
        HomeUI.SetActive(true);
    }

    void OnEnable()
    {
        OnHome();
    }
}
