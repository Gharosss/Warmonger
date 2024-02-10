using UnityEngine;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Text tooltipText;
    private static TooltipScript instance;
    private RectTransform backgroundRectTransform;
    [SerializeField]
    private Camera UICamera;

    private void Start() {
        HideTooltip();

    }
    private void Update() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, UICamera, out localPoint);
        if(localPoint.x > -170) {
             transform.localPosition = new Vector2 (-170, localPoint.y);
        }
        else    transform.localPosition = localPoint;
    }
    private void Awake() {
        instance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
        ShowTooltip("123123 text text");
    }
    private void ShowTooltip(string text) {
        gameObject.SetActive(true);
        tooltipText.text = text;
        float textPaddingSize = 8f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f) ;
        backgroundRectTransform.sizeDelta = backgroundSize;
    }
    private void HideTooltip() {
        gameObject.SetActive(false);
    }
    public void ShowTooltipStatic(string text) {
        instance.ShowTooltip(text);
    }
    public void HideTooltipStatic() {
        instance.HideTooltip();
    }


}
