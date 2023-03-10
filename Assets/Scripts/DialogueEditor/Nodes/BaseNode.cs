using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node {

	public virtual string GetString() {
		return null;
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null;
	}

	public virtual string GetNodeType() {
		return null;
	}
}