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

    [Header("Text File")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("Head Image")]
    public Sprite headPlayer;
    public Sprite headNPC;

    bool textFinished;
    bool isTyping;

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
    }

    void OnEnable()
    {
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
                index = 0;
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

    void GetTextFromFile(TextAsset file)
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
                index++;
                break;
            case "B":
                headImage.sprite = headNPC;
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