using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingObject : MonoBehaviour
{
    [SerializeField]
    float hp = 1;
    [SerializeField]
    float score;

    [SerializeField] 
    float[] crushing_Condition = { 1f };

    [SerializeField]
    Sprite[] objLevel = new Sprite[1];

    SpriteRenderer rend;
    Animator animator;
    AudioSource audioSource;

    QuestManager questManager;

    int currentLevel = 0;
    bool IsAnimation = false;
    

    void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        
        rend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
            IsAnimation = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
            Crush();
    }

    public void Crush()
    {
        if(hp > 0)
        {
            hp -= 1f;
            
            questManager.ScoreSum += score;

            if (currentLevel < crushing_Condition.Length && hp <= crushing_Condition[currentLevel])
            {
                if (IsAnimation)
                {
                    currentLevel += 1;
                    animator.SetInteger("Level", currentLevel);
                }

                else
                {
                    rend.sprite = objLevel[currentLevel];
                    currentLevel += 1;
                }
                if(currentLevel >= crushing_Condition.Length)
                {
                    if(animator != null)
                        animator.SetTrigger("IsCrush");

                    audioSource.Play();
                    if(GetComponent<TutorialUI>() != null)
                        GetComponent<TutorialUI>().OffUI();
                    questManager.QuestNumber += 1;
                }
            }
        }
    }
    
    
}
