using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClueIconTrigger : MonoBehaviour, IPointerClickHandler
{
    public Sprite imageSource;
    public Image clueImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        clueImage.sprite = imageSource;
        clueImage.enabled = true;
        GetComponent<Animator>().SetBool("Update", false);
    }
}
