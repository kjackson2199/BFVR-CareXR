using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidController : MonoBehaviour
{
    public float FillSpeed = 1.0f;

    public float minFill = 0.0f;

    public float maxFill = 0.25f;
    public Material matFluid;
    public int matFluidIndex = 0;

    public float startPercentFill = 0.0f;

    public bool InvertAtStart = false;

    private Renderer render;

    private bool _useMat = false;

    private Coroutine _matUpdate;


    // Start is called before the first frame update
    void Start()
    {

        render = GetComponent<Renderer>();
        if (matFluid != null)
        {
            _useMat = true;
        }
        SetInvertFill(InvertAtStart);
        SetPercentFillTo(startPercentFill);
    }

    void OnEnable()
    {
        SetPercentFillTo(startPercentFill);
        if (_matUpdate != null) StopCoroutine(_matUpdate);
    }
    void OnDisable()
    {
        if (_matUpdate != null) StopCoroutine(_matUpdate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignMaterial(Material mat)
    {
        AssignMaterialAdvanced(mat, true);
    }

    public void AssignMaterialAndInvert(Material mat)
    {
        AssignMaterialAdvanced(mat, true, 1f, true);
    }


    public void AssignMaterialAdvanced(Material mat, bool resetFill = false, float overrideResetFillPercent = float.NaN, bool resetInvert = false)
    {
        // if (render != null){
        //     if (_matUpdate != null) StopCoroutine(_matUpdate);
        //     SetMatFluid(resetInvert);
        // } else {
        //     _matUpdate = StartCoroutine(WaitSetMatFluid(resetInvert));
        // }
        if (render != null)
        {
            if (_matUpdate != null) StopCoroutine(_matUpdate);
            matFluid = mat;
            SetMatFluid(resetInvert);
            if (resetFill) SetPercentFillTo(float.IsNaN(overrideResetFillPercent) ? startPercentFill : overrideResetFillPercent);
        }
        else
        {
            _matUpdate = StartCoroutine(WaitAssignMaterialAdvanced(mat, resetFill, overrideResetFillPercent, resetInvert));
        }
    }

    public void AnimateFluidLevel(float percentFull)
    {
        AnimateFluidLevelAdvanced(percentFull, true, 0f);
    }

    public void AnimateFluidLevelInverted(float percentFull)
    {
        AnimateFluidLevelAdvanced(percentFull, true, 0f, null, true);
    }

    public void AnimateFluidLevelDown(float percentFull)
    {
        AnimateFluidLevelAdvanced(percentFull, true, 1f);
    }

    public void Reset(float fill = -1)
    {
        if (fill < 0) fill = startPercentFill;
        SetPercentFillTo(fill);
    }
    public void ResetInvert(float fill = -1)
    {
        SetInvertFill(true);
        if (fill < 0) fill = startPercentFill;
        SetPercentFillTo(fill);
    }

    public void AnimateFluidLevelAdvanced(float percentFull, bool resetFillLevel = false, float resetPercentFull = 0.0f, Material assignMatOnStart = null, bool invertFill = false)
    {
        Debug.LogFormat("{0}.AnimateFluidLevelAdvanced: percentFull={1}, invert={2}", gameObject.name, percentFull, invertFill);
        if (assignMatOnStart) AssignMaterialAdvanced(assignMatOnStart, resetFillLevel);
        // rend.material.SetFloat("_WobbleZ", wobbleAmountZ);
        SetInvertFill(invertFill);
        float from = 0f;
        float to = 0f;
        if (invertFill)
        {
            from = (percentFull * (maxFill - minFill)) + minFill;
            to = resetFillLevel ? (resetPercentFull * (maxFill - minFill)) + minFill : CurrentFill;
        }
        else
        {
            from = resetFillLevel ? (resetPercentFull * (maxFill - minFill)) + minFill : CurrentFill;
            to = (percentFull * (maxFill - minFill)) + minFill;
        }
        StartCoroutine(FillTo(from, to));
    }

    IEnumerator FillTo(float start, float end)
    {
        Debug.LogFormat("[{0}] FillTo: from {1}, to {2}", gameObject.name, start, end);
        float delta = 0.0f;//start >= end ? 1.0f : 0.0f;        
        while (delta < 1.0f)//} && delta > 0.0f)
        {
            delta += Time.deltaTime * FillSpeed;
            Debug.LogFormat("[{0}] FillTo: delta={1}", gameObject.name, delta);
            delta = Mathf.Clamp(delta, 0.0f, 1.0f);
            float fillTo = Mathf.Lerp(start, end, delta);
            // if (fillTo > end) fillTo = end;
            if (fillTo > maxFill) fillTo = maxFill;
            else if (fillTo < minFill) fillTo = minFill;
            SetFillTo(fillTo);
            yield return null;
            // yield return new WaitForEndOfFrame();
        }
        Debug.LogFormat("[{0}] FillToComplete", gameObject.name);
    }

    void SetPercentFillTo(float perc)
    {
        float fill = (perc * (maxFill - minFill)) + minFill;
        SetFillTo(fill);
    }

    void SetFillTo(float value)
    {
        if (_useMat)
        {
            if (matFluid != null) matFluid.SetFloat("_Fill", value);
        }
        else
        {
            if (render != null) render.materials[matFluidIndex].SetFloat("_Fill", value);
        }
    }

    void SetInvertFill(bool value)
    {
        if (_useMat)
        {
            if (matFluid != null) matFluid.SetFloat("_InvertFill", value ? 1f : 0f);
        }
        else
        {
            if (render != null) render.materials[matFluidIndex].SetFloat("_InvertFill", value ? 1f : 0f);
        }
    }

    IEnumerator WaitSetMatFluid(bool resetInvert = false)
    {
        if (render == null) yield return new WaitForEndOfFrame();
        SetMatFluid(resetInvert);
    }

    IEnumerator WaitAssignMaterialAdvanced(Material mat, bool resetFill = false, float overrideResetFillPercent = float.NaN, bool resetInvert = false)
    {
        if (render == null) yield return new WaitForEndOfFrame();
        AssignMaterialAdvanced(mat, resetFill, overrideResetFillPercent, resetInvert);
    }

    void SetMatFluid(bool resetInvert = false)
    {
        Material[] curr = render.materials;
        curr[matFluidIndex] = matFluid;
        render.materials = curr;
        render.materials[matFluidIndex].SetFloat("_InvertFill", resetInvert ? 1f : 0f);
    }

    float CurrentFill
    {
        get
        {
            float currentFill = _useMat ? matFluid.GetFloat("_Fill") : render.materials[matFluidIndex].GetFloat("_Fill");
            return currentFill;
        }
    }

    public float CurrentFillPercent()
    {
        float perc = (CurrentFill - minFill) / (maxFill - minFill);
        return perc;
    }

    void OnDestroy()
    {
        SetPercentFillTo(startPercentFill);
    }
}
