using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClueImageTrigger : MonoBehaviour, IPointerClickHandler
{
    public Image clueImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        clueImage.sprite = null;
        clueImage.enabled = false;
    }
}
