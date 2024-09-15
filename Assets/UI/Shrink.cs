using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour
{
    private bool isPressed;
    public Leaf[] leafs;

    public GameObject dimmer;
    public GameObject riddleUIPanel;
    public GameObject settingsUIPanel;
    public GameObject friendsUIPanel;

    public void OnPointerUp() {
        isPressed = false;
        transform.localScale /= 0.7f;
        foreach (Leaf leaf in leafs) {
            leaf.OnPointerUp();
            if(leaf.isHovering) {
                dimmer.SetActive(!dimmer.activeSelf);
                if(leaf.gameObject.name == "TasksLeaf") {
                    riddleUIPanel.SetActive(!riddleUIPanel.activeSelf);   
                } else if (leaf.gameObject.name == "SettingsLeaf") {
                    // Debug.Log("cookie");
                    settingsUIPanel.gameObject.SetActive(!settingsUIPanel.activeSelf);
                } else if (leaf.gameObject.name == "FriendsLeaf") {
                    // Debug.Log("cookie");
                    friendsUIPanel.gameObject.SetActive(!friendsUIPanel.activeSelf);
                }
            }
        }
    }

    public void OnPointerDown() {
        isPressed = true;
        transform.localScale *= 0.7f;
        foreach (Leaf leaf in leafs) {
            leaf.OnPointerDown();
        }
    }
}
