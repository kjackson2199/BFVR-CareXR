using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR
{
    /// <summary>
    /// Math extentions not found in default Mathf struct
    /// </summary>
    public struct BFVRMathExtentions
    {
        public static float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
        {
            return targetFrom + (source - sourceFrom) * (targetTo - targetFrom) / (sourceTo - sourceFrom);
        }
    }
}

