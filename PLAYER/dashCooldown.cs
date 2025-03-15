using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCooldown : MonoBehaviour
{
    public float coolDownTime = 2;
    private float nextDashTime = 0;

    // Update is called once per frame
    private void Update()
    {
        if(Time.deltaTime  > nextDashTime)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                print("dash used, cooldown started");
                nextDashTime =  Time.deltaTime + coolDownTime;
            }
        }
    }
}
