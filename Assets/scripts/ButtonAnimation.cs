using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        button.transform.localScale = Vector3.one * 0.9f;
        Invoke(nameof(ResetScale), 0.1f);
    }

    void ResetScale()
    {
        button.transform.localScale = Vector3.one;
    }
}
