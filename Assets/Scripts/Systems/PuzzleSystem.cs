using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSystem : MonoBehaviour
{
    public GameObject icon;
    public GameObject panel;
    public Image clueImage;

    private Dictionary<int, GameObject> clues = new Dictionary<int, GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        clueImage.enabled = false;
    }

    public void UpdateClue(int id, int childId)
    {
        var clue = clues[id] as GameObject;
        var trigger = clue.GetComponent<PuzzleIconTrigger>();

        // NEED TO BE MODIFIED "UI/Clues/c"+id+"-"+childId
        trigger.imageSource = Resources.Load<Sprite>("UI/paper");
    }

    public void InsertClue(int id, int childId)
    {
        var clue = Instantiate(icon) as GameObject;
        clue.transform.SetParent(panel.transform, false);
        var trigger = clue.GetComponent<PuzzleIconTrigger>();

        // NEED TO BE MODIFIED "UI/Clues/c"+id+"-"+childId
        trigger.imageSource = Resources.Load<Sprite>("UI/paper");
        trigger.clueImage = clueImage;

        clues.Add(id, clue);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // JUST FOR TEST! NEED TO BE DELETED!
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            var createImage = Instantiate(icon) as GameObject;
            createImage.transform.SetParent(panel.transform, false);
            var trigger = createImage.GetComponent<PuzzleIconTrigger>();
            trigger.imageSource = Resources.Load<Sprite>("UI/paper");
            trigger.clueImage = clueImage;
        }

        
    }
}
