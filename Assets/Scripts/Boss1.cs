using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float[] EpicCondition = { 25f, 15f, 10f };

    [SerializeField]
    AudioClip[] attackAudio;
    
    [SerializeField]
    GameObject player;

    Rigidbody2D rigid2D;
    Animator animator;
    AudioSource audioPlayer;

    float ctime = 0;

    int epicLevel = 0;
    int direction = 0;
    int epicPatten = 0;

    bool isStop = false;
    bool invincibility = false;
    bool epicOn = false;

    public bool IsDie
    {
        get;
        set;
    }


    int damge = 2;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        direction = player.transform.position.x > transform.position.x ? 1 : -1;
        ctime += Time.deltaTime;

        if(!isStop)
        {
            changDir();
            if (1.5f < Vector3.Distance(transform.position, player.transform.position))
            {
                animator.SetBool("IsWalk", true);
            }

            else
            {
                animator.SetBool("IsWalk", false);
                rigid2D.velocity = Vector2.zero;
                if (ctime >= 2f)
                {
                    AttackOn();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isStop)
        {
            if (1.5f < Vector3.Distance(transform.position, player.transform.position))
            {
                rigid2D.velocity = new Vector2(direction * moveSpeed, rigid2D.velocity.y);
            }
        }
    }

    void AttackOn()
    {
        int patten = Random.Range(1, 3);
        ctime = 0;
        isStop = true;
        rigid2D.velocity = Vector2.zero;

        if(patten == 1)
        {
            damge = 2;
            animator.SetTrigger("IsAttack1");
        }

        else
        {
            damge = 3;
            animator.SetTrigger("IsAttack2");
        }
    }

    public void AttackOff()
    {
        if(!epicOn)
        {
            isStop = false;
            damge = 2;
            ctime = 1f;
        }
    }

    public void Attack1()
    {
        Collider2D[] _col = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(direction * 0.75f, 0f), new Vector2(1.5f, 2.9f),0f);

        playAudio(0);

        for(int i = 0; i< _col.Length; i++)
        {
            if(_col[i].CompareTag("Player"))
            {
                player.GetComponent<Player>().Hurt(damge);
            }
        }
    }

    public void Attack2()
    {
        Collider2D[] _col = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(direction * 1f, 0f), new Vector2(1.7f, 2.9f), 0f);

        playAudio(1);

        for (int i = 0; i < _col.Length; i++)
        {
            if (_col[i].CompareTag("Player"))
            {
                player.GetComponent<Player>().Hurt(damge);
            }
        }
    }

    public void EpicAttack()
    {
        playAudio(2);
        if(epicPatten == 1)
        {
            if (player.transform.position.x > 29f)
            {
                player.GetComponent<Player>().Hurt(5);
            }
        }

        else if(epicPatten == 2)
        {
            if(player.transform.position.x < 52)
            {
                player.GetComponent<Player>().Hurt(5);
            }
        }
    }

    public void EpicAttackOff()
    {
        invincibility = false;
        isStop = false;
        epicOn = false;
        ctime = 1f;
    }

    void changDir()
    {
        if (direction > 0)
        {
            float dir = -1 * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }

        else if (direction < 0)
        {
            float dir =  Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }
    }

    public override void Hurt(int Damge)
    {
        if(!invincibility)
        {
            base.Hurt(Damge);
            if (hp <= 0)
            {
                animator.SetTrigger("Die");
                isStop = true;
                rigid2D.velocity = Vector2.zero;
                invincibility = true;
            }

            else if (epicLevel < EpicCondition.Length && hp <= EpicCondition[epicLevel])
            {
                epicLevel += 1;
                rigid2D.velocity = Vector2.zero;
                epicOn = true;
                StartCoroutine("EpicOn");
            }
        }
            
    }

    void dieBoss()
    {
        IsDie = true;
        gameObject.SetActive(false);
    }
    IEnumerator EpicOn()
    {
        invincibility = true;
        isStop = true;
        rigid2D.velocity = Vector2.zero;
        int patten = Random.Range(1, 3);
        epicPatten = patten;
        Vector2 Destination = Vector2.zero;
        if (patten == 1)
        {
            Destination = new Vector2(29, transform.position.y);
        }

        else if(patten == 2)
        {
            Destination = new Vector2(52, transform.position.y);
        }

        if(Destination.x > transform.position.x)
        {
            float dir = -1 * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }

        else
        {
            float dir = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }

        animator.SetBool("IsWalk", true);
        while(transform.position.x != Destination.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, 2 * moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        animator.SetBool("IsWalk", false);
        rigid2D.velocity = Vector2.zero;

        if (patten == 1)
        {
            float dir = -1 * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }

        else
        {
            float dir = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }

        animator.SetTrigger("IsEpicAttack");
        yield return null;
    }

    void playAudio(int index)
    {
        if (index == 0)
        {
            audioPlayer.clip = attackAudio[0];
        }

        else if (index == 1)
        {
            audioPlayer.clip = attackAudio[1];
        }

        else if (index == 2)
        {
            audioPlayer.clip = attackAudio[2];
        }

        audioPlayer.Play();
    }
}
