using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class GetFlagNode : BaseNode
{
	[Input] public Connection input;
	[Output] public Connection trueExit;
	[Output] public Connection falseExit;

	public string flagName;
	//public Sprite sprite;
	public override string GetString()
	{ //overriding allows you to create a broad type of object that you can refer to but then get specific data from sub objects  
		return "GetFlagNode/" + flagName;
	}
	public override object GetValue(NodePort port)
	{
		return null;
	}

	/* public override Sprite GetSprite(){
		return sprite;

	}  */
}