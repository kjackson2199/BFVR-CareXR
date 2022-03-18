using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimationTextDirections : MonoBehaviour
{
    public Text Target;
    public TMPro.TMP_Text TMPTarget;

    public List<string> Directions = new List<string>();

    public bool ShowNextOnAwake = false;

    private int _index = -1;

    public delegate void OnShowNextDelegate(string text);
    public static OnShowNextDelegate onShowNextEvent;

    void Start()
    {
        
    }

    void Awake(){
        _index = -1;
        if (Target != null) Target.text = "";
        if (TMPTarget != null) TMPTarget.text = "";
        if (ShowNextOnAwake) ShowNext();
    }

    public void ShowNext(){
        _index += 1;
        if (_index < Directions.Count){
            if (Target != null) Target.text = Directions[_index];
            if (TMPTarget != null) TMPTarget.text = Directions[_index];
            if (onShowNextEvent != null) onShowNextEvent.Invoke(Directions[_index]);
        }
    }

    public void ShowStep(int stepIndex)
    {
        if (stepIndex < 0)
        {
            _index = 0;
        }
        else if (stepIndex > Directions.Count - 1)
        {
            _index = Directions.Count - 1;
        }

        else
        {
            _index = stepIndex;
        }

        if (Target != null) Target.text = Directions[_index];
        if (TMPTarget != null) TMPTarget.text = Directions[_index];
        if (onShowNextEvent != null) onShowNextEvent.Invoke(Directions[_index]);
    }

    public void Reset(bool showNext = false){
        _index = -1;
        if (Target != null) Target.text = "";
        if (TMPTarget != null) TMPTarget.text = "";
        if (showNext) ShowNext();
    }
}
