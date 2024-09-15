using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonPanel : MonoBehaviour {

    public TextMeshProUGUI nametext;
    public int playerNum;


    private void OnEnable() {
        Player[] players = PhotonNetwork.PlayerList;

        if(playerNum+1 == PhotonNetwork.LocalPlayer.ActorNumber) {
            playerNum = 3;
        }

        if(playerNum >= players.Length ) {
            gameObject.SetActive(false);
            return;
        }
        nametext.text = players[playerNum].NickName;
    }
}
