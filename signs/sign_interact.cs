using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sign_interact : MonoBehaviour
{
    string popUp;
    public GameObject PopUpBox;
    public void signInteract() {
       
            if (Input.GetKeyDown(KeyCode.E))
            {

                PopUpSystem pop = GameObject.FindGameObjectWithTag("Player").GetComponent<PopUpSystem>();
                if (popUp != null)
                {
                    pop.PopUp(popUp);
                }
                PopUpBox.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {

                PopUpBox.SetActive(false);
            }
        
    }
}
