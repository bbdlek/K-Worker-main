using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    DoorManager doorManager;

    [SerializeField]
    JoystickMove joystickMove;
    [SerializeField]
    HitButton hitButton;

    [SerializeField]
    public float hp = 10;
    public float Hp
    {
        get { return hp; }
    }
    public float MaxHp
    {
        get;
        set;
    }
    [SerializeField]
    float moveSpeed = 3.0f;

    [SerializeField]
    AudioClip[] attackAudio;


    bool inDoor = false;
    bool isStop = false;

    Collider2D doorYes;

    Animator animator;
    Rigidbody2D rigid2D;
    AudioSource audioPlayer;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

        MaxHp = hp;
    }

    void FixedUpdate()
    {
        animator.SetInteger("IsWalk", joystickMove.H);
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Kick"))
        {
            rigid2D.velocity = Vector2.zero;
        }

        else
        {
            rigid2D.velocity = new Vector2(joystickMove.H * moveSpeed, rigid2D.velocity.y);
        }

        //테스트용
        //float h = Input.GetAxisRaw("Horizontal");

        //animator.SetInteger("IsWalk", (int)h);

        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Kick"))
        //{
        //    rigid2D.velocity = Vector2.zero;
        //}

        //else
        //{
        //    rigid2D.velocity = new Vector2(h * moveSpeed, rigid2D.velocity.y);
        //}
    }

    void Update()
    {
        if (!isStop)
        {
            ChageDir();
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                FindObjectOfType<QuestManager>().questNum += 1000;
            } 
            inDoor = true;
            doorYes = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            inDoor = false;
            doorYes = null;
        }
    }

    public void Attack(int index)
    {
        float dir = transform.localScale.x > 0 ? 1 : -1;
        isStop = true;
        rigid2D.velocity = Vector2.zero;
        Collider2D[] _col = null;
        int damge = 1;

        switch (index)
        {
            case 0:
                 _col = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(dir * 0.14f, 0.01f) * 5, new Vector2(1.2f, 2.9f),0f);
                break;
            case 1:
                 _col = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(dir * 0.14f, 0.01f) * 5, new Vector2(1.2f, 2.9f), 0f);
                break;
            case 2:
                damge = 2;
                 _col = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(dir * 0.12f, 0.01f) * 5, new Vector2(1f, 2.9f), 0f);
                break;
            case 3:
                damge = 3;
                _col = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(dir * 0.12f, 0.01f) * 5, new Vector2(1.3f, 2.9f), 0f);
                break;
        }

        playAudio(index);

        for (int i = 0; i < _col.Length; i++)
        {

            if (_col[i].CompareTag("Enemy"))
            {
                _col[i].GetComponent<Enemy>().Hurt();
            }

            else if (_col[i].CompareTag("CrushingObject"))
            {
                _col[i].GetComponent<CrushingObject>().Crush();
            }

            else if (_col[i].CompareTag("Boss"))
            {
                _col[i].GetComponent<Boss>().Hurt(damge);
            }

            else if (_col[i].CompareTag("Door"))
            {
                if (inDoor && doorYes != null)
                {
                    doorManager.MoveByDoor(doorYes.GetComponent<Door>().DoorNum);
                }
            }
        }
    }

    public void AttackOff()
    {
        isStop = false;
    }

    void ChageDir()
    {
        if(joystickMove.H > 0)
        {
            float dir = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }

        else if (joystickMove.H < 0)
        {
            float dir = -1 * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Hurt(int Damge)
    {
        hp -= Damge;

        if(hp <= 0)
        {
            if (!DBManager.instance.hasChance)
                UIManager.instance.endPanel.SetActive(true);
            else UIManager.instance.endPanel2.SetActive(true);
            //AdMobManager.instance.ShowRewardAds();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
   
    void playAudio(int index)
    {
        if(index == 0)
        {
            audioPlayer.clip = attackAudio[0];
        }
        
        else if(index == 1)
        {
            audioPlayer.clip = attackAudio[1];
        }

        else if (index == 2 || index == 3)
        {
            audioPlayer.clip = attackAudio[2];
        }

        audioPlayer.Play();
    }
}
