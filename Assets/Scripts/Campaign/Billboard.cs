using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Range(0.1f, 4.0f)] public float radius = 1.0f;

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.localScale = radius * Vector3.one;
    }
}
