using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFX : MonoBehaviour
{
    public float power = -0.02f;

    void Update()
    {
        var mpos = Input.mousePosition;
        mpos.x = (mpos.x / Screen.width * 2 - 1) * -power;
        mpos.y = (mpos.y / Screen.height * 2 - 1) * -power;
        Shader.SetGlobalVector("_MousePos", new Vector4(mpos.x, mpos.y, 0, 0));
    }
}
