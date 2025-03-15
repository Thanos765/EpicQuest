using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck2 : MonoBehaviour
{
    
     public string tagTarget = "Player";
public List<Collider2D> detectedObjs = new List<Collider2D>();
    Boss2 boss2;
    public AudioSource AudioSource;
    Animator anim;
    private void Awake()
    {
        
        boss2 = GetComponentInParent<Boss2>();
        anim=GetComponentInParent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == tagTarget)
        {
            anim.SetTrigger("InAggroRange");
            boss2.SetAggroStatus(true);
            detectedObjs.Add(collision);
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {
            boss2.SetAggroStatus(false);
             anim.SetTrigger("OutOfAggroRange");
            detectedObjs.Remove(collision);
            AudioSource.Stop();
        }
    }
    
}