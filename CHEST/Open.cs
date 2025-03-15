using UnityEngine;

public class Open : MonoBehaviour
 {
    
    public Animator anim;


    void Start (){
       anim = GetComponent<Animator>();
    }


    public void openChest(){
        anim.SetBool("playerInteract",true);
    }

}
  

