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
    public string text;
    public List<DialogueChoice> choices;
}

[Serializable]
public class DialogueChoice
{
    public string text;
    public string nextId;
}
