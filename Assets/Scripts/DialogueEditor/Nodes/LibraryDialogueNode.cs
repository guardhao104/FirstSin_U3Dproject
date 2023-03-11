using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class LibraryDialogueNode : BaseNode {
	[Input] public Connection input;
	[Output] public Connection exit;

	[TextArea] public string dialogueLine;
	//public Sprite sprite;
	public override string GetString()
	{ //overriding allows you to create a broad type of object that you can refer to but then get specific data from sub objects  
		return "LibraryDialogueNode/" + dialogueLine;
	}
	public override object GetValue(NodePort port)
	{
		return null;
	}
}