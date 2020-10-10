using UnityEngine;
using UnityEngine.UI;

class Popup : MonoBehaviour
{
    [SerializeField] GameObject Holder;
    [SerializeField] Text Heading;
    [SerializeField] Text Content;

    public static bool IsActive => Instance.Holder.activeInHierarchy;

    static Popup Instance;

    void Awake()
    {
        Instance = this;
        Holder.SetActive(false);
    }

    public static void ShowMessage(string heading,string content)
    {
        Instance.Heading.text = heading;
        Instance.Content.text = content;
        Instance.Holder.SetActive(true);
    }
}
