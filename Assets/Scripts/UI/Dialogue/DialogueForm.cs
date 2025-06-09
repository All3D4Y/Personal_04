using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueForm
{
    public string id;
    public string English;
    public string Korean;
}

[System.Serializable]
public class DialogueWrap
{
    public List<DialogueForm> dialogues;
}
