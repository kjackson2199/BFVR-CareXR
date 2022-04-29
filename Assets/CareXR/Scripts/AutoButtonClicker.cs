using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoButtonClicker : MonoBehaviour
{
    Button b;

    void Awake()
    {
        b = GetComponent<Button>();
    }

    void Update()
    {
        b.onClick.Invoke();
    }
}
