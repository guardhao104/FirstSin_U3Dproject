using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClueIconTrigger : MonoBehaviour, IPointerClickHandler
{
    public Sprite imageSource;
    public Image clueImage;
    public AudioSource soundEffect;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clueImage.enabled == false)
        {
            OpenClue();
        }
        else if (clueImage.sprite != imageSource)
        {
            OpenClue();
        }
    }
    
    private void OpenClue()
    {
        clueImage.sprite = imageSource;
        clueImage.enabled = true;
        GetComponent<Animator>().SetBool("Update", false);
        soundEffect.Play();
    }
}