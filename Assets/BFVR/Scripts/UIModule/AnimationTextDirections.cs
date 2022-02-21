using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimationTextDirections : MonoBehaviour
{
    public Text Target;
    //public BFVRPopupTagManager PopupTagManager;
    public TMPro.TMP_Text TMPTarget;

    public List<string> Directions = new List<string>();

    public bool ShowNextOnAwake = false;

    private int _index = -1;
    // Start is called before the first frame update


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
            //if (PopupTagManager) PopupTagManager.SetCurrentDirectionText(Directions[_index]);
        }
    }

    public void Reset(bool showNext = false){
        _index = -1;
        if (Target != null) Target.text = "";
        if (TMPTarget != null) TMPTarget.text = "";
        //if (PopupTagManager != null) PopupTagManager.ResetTags();
        if (showNext) ShowNext();
    }
}
