using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] gameobj;
    public void CloseObject() {
        foreach (GameObject obj in gameobj) {
            obj.SetActive(false);
        }
    }
}
