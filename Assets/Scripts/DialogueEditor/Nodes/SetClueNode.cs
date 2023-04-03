using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SetClueNode : BaseNode {
	[Input] public Connection input;
	[Output] public Connection exit;

	public int clueId;
	public int clueChildId;

	public override string GetString()
	{
		return "SetClueNode/" + clueId + "/" + clueChildId;
	}
	public override object GetValue(NodePort port)
	{
		return null;
	}
}