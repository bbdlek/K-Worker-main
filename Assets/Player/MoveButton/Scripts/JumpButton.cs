using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    float v = 0f;
    public float V
    {
        get { return v; }
    }

    public void DownJump()
    {
        v = 1f;
    }

    public void UpJump()
    {
        v = 0f;
    }
}
