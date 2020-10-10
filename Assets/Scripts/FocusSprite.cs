using UnityEngine;

class FocusSprite : MonoBehaviour
{
    [SerializeField] float Speed;
    float Rotation
    {
        get => transform.localEulerAngles.z;
        set
        {
            transform.localEulerAngles = new Vector3(0, 0, value);
        }
    }
    void Update()
    {
        Rotation += Time.deltaTime * Speed;
    }
}
