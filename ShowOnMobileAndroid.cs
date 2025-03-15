using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnMobileAndroid : MonoBehaviour
{
  // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }


}

