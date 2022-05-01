using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    protected float hp = 15f;
    public float Hp
    {
        get { return hp; }
    }
    public float MaxHp
    {
        get;
        set;
    }

    private void Awake()
    {
        MaxHp = hp;
    }

    public virtual void Hurt(int Damge)
    {
        hp -= Damge;
    }
}
