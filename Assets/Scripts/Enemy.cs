using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float hp = 3;
    [SerializeField]
    float moveSpeed = 3f;
    [SerializeField]
    float score = 100f;

    float ctime = 0f;
    int dir = 0;

    bool stop = false;

    QuestManager questManager;
    Rigidbody2D rigid2D;
    Animator animator;

    public bool isStage2;

    void Awake()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        ////�׽�Ʈ��
        //if (Input.GetKeyDown(KeyCode.J))
        //    Hurt();d

        if (isStage2)
        {
            dir = -1;
            animator.SetInteger("IsWalk", dir);
        }
        if (!stop && !isStage2)
        {
            ctime += Time.deltaTime;

            if (ctime >= 3f)
            {
                ctime = 0;
                dir = Random.Range(-1, 2);
            }

            animator.SetInteger("IsWalk", dir);
            
            ChageDir();
        }
    }
    void FixedUpdate()
    {
        if (hp > 0)
        {
            rigid2D.velocity = new Vector2(dir * moveSpeed, rigid2D.velocity.y);
        }
        else
        {
            if (isStage2) rigid2D.AddForce(new Vector2(1, 1), ForceMode2D.Impulse);
        }

    }

    public void Hurt()
    {
        hp -= 1;
        if (!stop)
        {
            if (hp > 0)
            {
                animator.SetTrigger("IsHurt");
                animator.SetBool("Hurting", true);
                dir = 0;
                ctime = 0;
            }
            else
            {
                if (isStage2)
                {
                    this.gameObject.layer = 7;
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(transform.DORotate(new Vector3(0, 0, -90), 2f, RotateMode.Fast))
                    .OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                }
                else
                {
                    questManager.QuestNumber += 1;
                    questManager.ScoreSum += score;
                    stop = true;
                    Destroy(gameObject);
                    dir = 0;
                }

            }
        }
    }

    void ChageDir()
    {
        if (dir > 0)
        {
            float _dir = -1 * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(_dir, transform.localScale.y, transform.localScale.z);
        }

        else if (dir < 0)
        {
            float _dir = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(_dir, transform.localScale.y, transform.localScale.z);
        }
    }

    public void HurtOff()
    {
        animator.SetBool("Hurting", false);
        ctime = 2.3f;
    }
}
