using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaf : MonoBehaviour
{

    private bool isPressed;

    public Transform initial;
    public Transform final;
    float percent = 0f;

    private void Start() {
        transform.position = initial.position;
        // Debug.Log(transform.position);
    }

    private void FixedUpdate() {
        if(isPressed) {
            // Debug.Log("Pressed");
            if(percent >= 0.999f) {
                percent = 1f;
                return;
            } else {
                percent += 0.2f;
            }
        } else {
            if(percent <= 0.001f) {
                percent = 0f;
                return;
            } else {
                percent -= 0.2f;
            }
        }

        // Debug.Log(percent);
        transform.position = initial.position + ((final.position - initial.position) * percent);
    }

    public void OnPointerDown() {
        isPressed = true;
        // gameObject.SetActive(true);
    }   
    
    public void OnPointerUp() {
        isPressed = false;
        // gameObject.SetActive(false);
    }

    [HideInInspector]
    public bool isHovering  = false;

    public void OnHoverEnter() {
        isHovering = true;
    }

    public void OnHoverExit() {
        isHovering = false;
    }
}