using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class DialogueData
{
    public string customerID;
    public List<DialogueNode> nodes;
}

[Serializable]
public class DialogueNode
{
    public string id;
    public string lines;
    public List<DialogueChoice> choices;
}
