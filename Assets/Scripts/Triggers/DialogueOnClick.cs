using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueOnClick : MonoBehaviour, IPointerClickHandler
{
    private NodeParser np;

    void Awake()
    {
        np = GameObject.Find("Player").GetComponent<NodeParser>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex > -1)
            {
                np.AnswerClicked(linkIndex);
            }
        }
    }
}
