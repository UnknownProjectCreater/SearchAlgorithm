using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommonButtonAnimController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    public void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Disabled");
    }
}
