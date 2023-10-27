using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    GameObject currentFloor;
    [SerializeField] int HP;
     [SerializeField] GameObject HpBar;
     [SerializeField]Text score;
     int point;
     float scoreTime;
     AudioSource deathSound;
     [SerializeField]GameObject Replay;
    // Start is called before the first frame update
    void Start()
    {
      HP = 5;
      point = 0;
      scoreTime = 0f;
      deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
        transform.Translate(movespeed*Time.deltaTime,0,0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
        transform.Translate(-movespeed*Time.deltaTime,0,0);
        }
        UpdateScore();
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
      if(other.gameObject.tag =="Normal")
      {
         if(other.contacts[0].normal == new Vector2(0f,1f))
         {
            currentFloor = other.gameObject;
            ModifyHP(2);
         }
      }
      else if(other.gameObject.tag =="Nails")
      {
         if(other.contacts[0].normal == new Vector2(0f,1f))
         {
            currentFloor = other.gameObject;
            ModifyHP(-3);
         }
      }
      else if(other.gameObject.tag =="Ceiling")
      {
         currentFloor.GetComponent<BoxCollider2D>().enabled = false;
         ModifyHP(-4);
      }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.tag =="death")
      {
         Die();
        
      }
    }
    
   void ModifyHP(int num)
   {
      HP += num;
      if(HP>5)
      {
         HP = 5;
      }
      else if(HP<=0)
      {
         HP = 0;
         Die();
      }
      UpdateHpBar();
   }

   void UpdateHpBar()
   {
       for(int i=0; i<HpBar.transform.childCount; i++)
       {
            if(HP>i)
            {
               HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
               HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }

       }
   }

   void UpdateScore()
   {
      scoreTime += Time.deltaTime;
      if(scoreTime > 2f)
      {
         point++;
         scoreTime = 0f;
         score.text = point.ToString();
      }
   }

   void Die()
   {
      deathSound.Play();
      Time.timeScale = 0f;
      Replay.SetActive(true);
   }

   public void Restart()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("SampleScene");

   }
}
