using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform image;

    // Start is called before the first frame update
    void Start()
    {
        image.GetComponent<Animator>().Play("DecreaseButton");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.GetComponent<Animator>().Play("IncreaseButton");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.GetComponent<Animator>().Play("DecreaseButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
