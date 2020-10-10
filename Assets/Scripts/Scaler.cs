using UnityEngine;

class Scaler : MonoBehaviour
{
    void Awake()
    {
        Camera Camera = GetComponent<Camera>();
        Camera.aspect = (float)Screen.width / Screen.height;
    }
}
