using System;
using UnityEngine;
using UnityEngine.UI;

class Popup : MonoBehaviour
{
    [SerializeField] GameObject Holder;
    [SerializeField] Text Heading;
    [SerializeField] Text Content;
    [SerializeField] Button Left;
    [SerializeField] Button Right;
    [SerializeField] Button Middle;

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
        Instance.Left.gameObject.SetActive(false);
        Instance.Right.gameObject.SetActive(false);
        Instance.Middle.gameObject.SetActive(true);
        Instance.Middle.transform.Find("Text").GetComponent<Text>().text = "OK";
    }

    public static void ShowConfirm(string heading,string content,Action yes, Action no)
    {
        Instance.Heading.text = heading;
        Instance.Content.text = content;
        Instance.Holder.SetActive(true);
        Instance.Left.gameObject.SetActive(true);
        Instance.Right.gameObject.SetActive(true);
        Instance.Middle.gameObject.SetActive(false);
        Instance.Left.transform.Find("Text").GetComponent<Text>().text = "YES";
        Instance.Right.transform.Find("Text").GetComponent<Text>().text = "NO";
        Instance.Left.onClick.RemoveAllListeners(); 
        if(yes!=null) 
            Instance.Left.onClick.AddListener(() => 
            {
                try { yes(); } catch (Exception e) { Debug.LogError(e); } Instance.Holder.SetActive(false);
            });
        Instance.Right.onClick.RemoveAllListeners(); 
        if(no!=null)
            Instance.Right.onClick.AddListener(() => 
            {
                try { no(); } catch (Exception e) { Debug.LogError(e); } Instance.Holder.SetActive(false);
            });
    }
}
