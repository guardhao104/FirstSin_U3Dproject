using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]
public struct Connection {}

public class ChoiceDialogueNode : BaseNode {
    [Input] public Connection input;
    //[Output] public Connection exit;
    [Output(dynamicPortList = true)] public List<string> Answers;
    public SpeakerType speakerType = SpeakerType.NPC;
    public enum SpeakerType { NPC, Player, Library, Description }
    public string speakerName;
    [TextArea] public string DialogueText;
     
    public override string GetString(){
        string value = "";
        switch (speakerType)
        {
            case SpeakerType.NPC: default: value += "NPCChoiceDialogueNode/"; break;
            case SpeakerType.Player: value += "PlayerChoiceDialogueNode/"; break;
            case SpeakerType.Library: value += "LibraryChoiceDialogueNode/"; break;
            case SpeakerType.Description: value += "DescriptionChoiceDialogueNode/"; break;
        }
		return value + speakerName + "/" + DialogueText + "/" + Answers[0];
	}

    public override object GetValue(NodePort port){
        return null;
    }


  
}
