using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStart : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine("Off");
    }

    public void TurnOff()
    {
        Destroy(gameObject);
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
