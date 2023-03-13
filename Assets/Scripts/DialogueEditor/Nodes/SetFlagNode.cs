using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SetFlagNode : BaseNode {
	[Input] public Connection input;
	[Output] public Connection exit;

	public string flagName;
	public bool flagValue;
	//public Sprite sprite;
	public override string GetString()
	{ //overriding allows you to create a broad type of object that you can refer to but then get specific data from sub objects  
		return "SetFlagNode/" + flagName + "/" + flagValue;
	}
	public override object GetValue(NodePort port)
	{
		return null;
	}

	/* public override Sprite GetSprite(){
		return sprite;

	}  */
}