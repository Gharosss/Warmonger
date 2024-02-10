
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // Start is called before the first frame update

    public Action MouseOverOnceFunc = null;
    public Action MouseOutOnceFunc = null;
    public virtual void OnPointerEnter(PointerEventData eventData) {
        if (MouseOverOnceFunc != null) MouseOverOnceFunc();
    }
    public virtual void OnPointerExit(PointerEventData eventData) {
        if (MouseOutOnceFunc != null) MouseOutOnceFunc();
    }
}
