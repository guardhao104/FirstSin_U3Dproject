using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph {
    public BaseNode start;
    public BaseNode current;
    public BaseNode initNode;

    public void Start() {
        start = initNode;
        current = initNode;
    }
	
}