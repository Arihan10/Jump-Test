using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleUIPanel : MonoBehaviour
{
    public Transform initial, final;
    private float percentage;

    private void OnEnable() {
        transform.position = initial.position;
        percentage = 0;
    }   

    private void FixedUpdate() {
        if(percentage >= 1) {
            percentage = 1;
            return;
        }

        transform.position = initial.position + (final.position - initial.position)*percentage;
        percentage += 0.1f;
    }

}
