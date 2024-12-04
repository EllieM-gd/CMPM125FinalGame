using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TestLinkHandler : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, null);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            Debug.Log("Opening URL: " + linkInfo.GetLinkID());
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
