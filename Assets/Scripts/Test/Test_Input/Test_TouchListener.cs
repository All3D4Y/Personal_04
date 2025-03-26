using System;
using UnityEngine;

public class Test_TouchListener : MonoBehaviour
{
    public bool testButton = false;


    Test_TouchIA touchIA;

    Vector2 touchStartPosition;
    Vector2 touchEndPosition;

    void Awake()
    {
        touchIA = GetComponent<Test_TouchIA>();
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }
}
