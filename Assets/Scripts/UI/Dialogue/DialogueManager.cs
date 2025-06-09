using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Language currentLanguage;

    Dictionary<string, Dictionary<string, string>> dialogueDB;

    public void LoadDialogueData(string json)
    {
        DialogueWrap parsed = JsonUtility.FromJson<DialogueWrap>(json);

        dialogueDB = parsed.dialogues.ToDictionary
            (
                d => d.id,
                d => new Dictionary<string, string>
                {
                    {"English", d.English },
                    {"Korean", d.Korean }
                }
            );
    }

    public void ShowDialogue(string id)
    {
        if (dialogueDB.TryGetValue(id, out var langMap))
        {
            if (langMap.TryGetValue(currentLanguage.ToString(), out string line))
            {
                dialogueText.text = $":{line}";
            }
            else
            {
                Debug.LogWarning("JSON error: language에 맞는 text 없음");
            }
        }
        else
        {
            Debug.LogWarning("JSON error: id에 맞는 data 없음");
        }
    }
}
