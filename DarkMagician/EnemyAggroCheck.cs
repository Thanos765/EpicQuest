using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class EnemyAggroCheck : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    Boss boss;
     Animator anim;
     GameObject MainCamera;
     public bool flag =false;
    public AudioSource AudioSource;

    public GameObject BossHealthUI;

    public BossFightMusicStart bossFightMusicStart;
    private void Awake(){
    boss = GetComponentInParent<Boss>();
    anim=GetComponentInParent<Animator>();
   
}


 private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == tagTarget)
        {
            BossHealthUI.SetActive(true);
            bossFightMusicStart.PlayAudio();
            flag=true;
           boss.SetAggroStatus(true);
           detectedObjs.Add(collision);
           anim.SetBool("inAggroRange",true);
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
           boss.SetAggroStatus(false);
            detectedObjs.Remove(collision);
            anim.SetBool("inAggroRange",false);
            AudioSource.Stop();
        }
    }




















}
