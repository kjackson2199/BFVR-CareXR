using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR {
public class BFVROrderedObjectAnimation : MonoBehaviour
{
    [System.Serializable]
    public struct AnimationStep { public string Name; public string StateName; }

    public Animator animator;

    public List<AnimationStep> AnimationSteps = new List<AnimationStep>();

    public bool AllowClipLoop = false;

    public string StartAtState = "";

    private Dictionary<string,AnimationStep> _steps = new Dictionary<string, AnimationStep>();
    private int _clipIndex = -1;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i<AnimationSteps.Count;i++){
            AnimationStep s = AnimationSteps[i];
            _steps[s.Name] = s;
        }
        // if (StartAtState != ""){
        //     PlayState(StartAtState,1f);
        // }
    }

    void OnEnable(){
        // if (_steps.ContainsKey(StartAtState)){
        //     PlayState(StartAtState,1f);
        // }
        CheckStateAtStart();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void Play(int index = 0){
        if (index < 0) return;
        AnimationStep s = AnimationSteps[index];     
        string stateName = s.StateName;      
        PlayState(stateName);
    }

    void PlayState(string stateName,float frameProgress = 0f){
        if (!stateName.Contains("."))
            stateName = "Base Layer."+stateName;

        Debug.LogFormat("[{0}] OrderedAnimation.Play: {1}",gameObject.name,stateName);
        animator.Play(stateName,-1,frameProgress);
    }

    private void CheckStateAtStart(){
        //check state names
        foreach(AnimationStep s in AnimationSteps){
            if (s.StateName.Equals(StartAtState)){
                PlayState(s.StateName,1f);    
                return;
            }
        }
        //check animation step names
        if (_steps.ContainsKey(StartAtState)){
            PlayState(_steps[StartAtState].StateName,1f);
        }
         
    }

    public void PlayAtIndex(int index = 0){
        if (index < AnimationSteps.Count){
            _clipIndex = index;
            Play(_clipIndex);
        }
    }

    public void PlayStep(string stepName){
        Debug.LogFormat("{0}.PlayStep: {1}",gameObject.name,stepName);
        if (_steps.ContainsKey(stepName)){
            AnimationStep s = _steps[stepName];
            _clipIndex = -1;
            for (int i = 0; i < AnimationSteps.Count; i++){
                if (AnimationSteps[i].Name == s.Name){
                    _clipIndex = i;
                    break;
                }
            }
            Debug.LogFormat("{0}.PlayStep: Play, clipIndex={1}",gameObject.name,_clipIndex);
            Play(_clipIndex);
        } else {
        Debug.LogFormat("{0}.PlayStep: stepName not found",gameObject.name);
        }
    }

    public void PlayNextClip(){
        if (_clipIndex+1 < AnimationSteps.Count){
            _clipIndex += 1;
            Play(_clipIndex);
        } else if (AllowClipLoop) {
            PlayAtIndex(0);
        }
    }

    public void Reset(){
        // if (StartAtState != ""){
        //     PlayState(StartAtState,1f);
        // }
        CheckStateAtStart();
    }


}
}