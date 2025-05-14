using UnityEngine;
using UnityEngine.UI;

public class ToggleBase : MonoBehaviour
{
    Toggle[] toggles;

    protected virtual void Awake()
    {
        toggles = new Toggle[transform.childCount];

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i] = transform.GetChild(i).GetComponent<Toggle>();
        }
    }
}
