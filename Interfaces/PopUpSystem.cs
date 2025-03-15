using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    public string[] text;
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public void PopUp(string text) {
        while (Input.GetKeyDown(KeyCode.E))
        {


            popUpBox.SetActive(true);
            popUpText.text = text;
            animator.SetTrigger("pop");
            
        }





    }

        
        

    
}
