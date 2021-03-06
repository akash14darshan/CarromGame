using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class Loader : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] Text Splash;
    [SerializeField] GameObject Play;
    [SerializeField] float FadeSpeed = 5f;
    [SerializeField] GameObject MainGame;

    float Alpha
    {
        get => Splash.color.a;
        set
        {
            Color color = Splash.color;
            Splash.color = new Color(color.r, color.g, color.b, Mathf.Clamp01(value));
        }
    }

    void Awake()
    {
        Alpha = 0f;
        Canvas.SetActive(true);
        Play.SetActive(false);
        MainGame.SetActive(false);
    }

    IEnumerator Start()
    {
        bool ShowSplash=false;
#if !UNITY_EDITOR
ShowSplash = true;
#endif
        if(ShowSplash)
        {
            while (Alpha != 1f)
            {
                Alpha += Time.deltaTime * FadeSpeed;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            while (Alpha != 0f)
            {
                Alpha -= Time.deltaTime * FadeSpeed;
                yield return null;
            }
        }
        Destroy(gameObject);
        MainGame.SetActive(true);
    }
}