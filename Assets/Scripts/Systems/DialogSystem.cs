using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [Header("UI Component")]
    public GameObject talkPanel;
    public Image headImage;
    public GameObject textLabel;
    public GameObject nameLabel;

    [Header("Text File")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("Head Image")]
    public Sprite headPlayer;
    public Sprite headNPC;
    public string nameNPC;

    bool textFinished;
    bool isTyping;

    List<string> textList = new List<string>();

    void OnEnable()
    {
        GetTextFromFile(textFile);
        index = 0;
        textFinished = true;
        StartCoroutine(setTextUI());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (index == textList.Count)
            {
                talkPanel.SetActive(false);
                return;
            }
            else
            {
                if (textFinished)
                {
                    StartCoroutine(setTextUI());
                }
                else if (!textFinished)
                {
                    isTyping = false;
                }
            }
        }
    }

    public void GetTextFromFile(TextAsset file)
    {
        textList.Clear();

        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator setTextUI()
    {
        textFinished = false;
        textLabel.GetComponent<TMP_Text>().text = "";

        switch (textList[index].Trim())
        {
            case "A":
                headImage.sprite = headPlayer;
                nameLabel.GetComponent<TMP_Text>().text = "";
                index++;
                break;
            case "B":
                headImage.sprite = headNPC;
                nameLabel.GetComponent<TMP_Text>().text = nameNPC;
                index++;
                break;
        }

        int word = 0;
        while (isTyping && word < textList[index].Length - 1)
        {
            textLabel.GetComponent<TMP_Text>().text += textList[index][word];
            word++;
            yield return new WaitForSeconds(textSpeed);
        }

        textLabel.GetComponent<TMP_Text>().text = textList[index];

        isTyping = true;
        textFinished = true;
        index++;
    }
}