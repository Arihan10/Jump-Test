using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    public float timeDelay = 1f;
    public GameObject obj;

    private void Start() {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(obj.activeSelf) {
            return;
        }

        if(timeDelay <= 0) {
            obj.SetActive(true);
        }
        timeDelay -= Time.fixedDeltaTime;
    }
}
