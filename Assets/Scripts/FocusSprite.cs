using UnityEngine;

class FocusSprite : MonoBehaviour
{
    [SerializeField] float Speed;

    [SerializeField] bool AnimateScale;
    [SerializeField] float MaxScale;
    private float MinScale;
    private float TargetScale;

    void Awake()
    {
        MinScale = transform.localScale.x;
    }

    void OnEnable()
    {
        if(AnimateScale)
        {
            Scale = MinScale;
            TargetScale = MaxScale;
        }
    }

    float Scale
    {
        get => transform.localScale.x;
        set
        {
            float newScale = Mathf.Clamp(value, MinScale, MaxScale);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

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
        if(AnimateScale)
        {
            Scale = Mathf.Lerp(Scale, TargetScale, Speed/4 * Time.deltaTime);
            if (Mathf.Approximately(Scale,MinScale))
                TargetScale = MaxScale;
            else if (Mathf.Approximately(Scale, MaxScale))
                TargetScale = MinScale;
        }
    }
}
