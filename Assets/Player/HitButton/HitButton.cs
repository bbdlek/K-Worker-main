using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    float ctime = 0f;
    public float Ctime
    {
        get { return ctime; }
        set { ctime = value; }
    }

    int attackLevel = 0;
   
    void Start()
    {
        
    }

    void Update()
    {
        Ctime += Time.deltaTime;
    }

    public void Attack()
    {
        if(Random.Range(0,2) == 0) Attack1();
        else Attack2();
    }

    public void Attack1()
    {
        animator.SetTrigger("IsAttack");
        if(ctime >= 0.2f)
        {
            if (ctime <= 1f)
            {
                ctime = 0;
                if (attackLevel > 2)
                {
                    attackLevel = 0;
                    animator.SetFloat("Blend", 0);
                    attackLevel += 1;
                }

                else
                {
                    animator.SetFloat("Blend", attackLevel);
                    attackLevel += 1;
                }
            }

            else
            {
                attackLevel = 0;
                animator.SetFloat("Blend", 0);
                ctime = 0;
            }
        }
    }

    public void Attack2()
    {
        if (ctime >= 0.5f)
        {
            attackLevel = 0;
            ctime = 0;
            animator.SetTrigger("IsKick");
        }
    }
}
