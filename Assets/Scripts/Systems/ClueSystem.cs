using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueSystem : MonoBehaviour
{
    public GameObject icon;
    public GameObject panel;
    public Image clueImage;
    public AudioSource soundEffect;

    private Dictionary<int, GameObject> clues = new Dictionary<int, GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        clueImage.enabled = false;
    }

    public void UpdateClue(int id, int childId)
    {
        var clue = clues[id] as GameObject;
        clue.GetComponent<Animator>().SetBool("Update", true);
        var trigger = clue.GetComponent<ClueIconTrigger>();
        trigger.imageSource = Resources.Load<Sprite>("UI/Clues/" + id + "-" + childId);
        soundEffect.Play();
    }

    public void InsertClue(int id, int childId)
    {
        var clue = Instantiate(icon) as GameObject;
        clue.transform.SetParent(panel.transform, false);
        var trigger = clue.GetComponent<ClueIconTrigger>();
        trigger.imageSource = Resources.Load<Sprite>("UI/Clues/" + id + "-" + childId);
        trigger.clueImage = clueImage;

        clues.Add(id, clue);
        soundEffect.Play();
    }

    public void RemoveClues()
    {
        foreach(KeyValuePair<int, GameObject> clue in clues)
        {
            Destroy(clue.Value);
            clues = new Dictionary<int, GameObject>();
        }
    }
}
