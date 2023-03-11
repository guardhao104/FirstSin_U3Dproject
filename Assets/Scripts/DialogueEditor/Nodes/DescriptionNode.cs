using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DescriptionNode : BaseNode {
	[Input] public Connection input;
	[Output] public Connection exit;

	[TextArea] public string descriptionText;
	//public Sprite sprite;
	public override string GetString()
	{ //overriding allows you to create a broad type of object that you can refer to but then get specific data from sub objects  
		return "DescriptionNode/" + descriptionText;
	}
	public override object GetValue(NodePort port)
	{
		return null;
	}
}