using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime; //*
using UnityEngine.SceneManagement; //*

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject lobbyPanel; //*
    public GameObject roomPanel; //*

    public InputField createInput;
    public InputField joinInput;
    public Text error;

    public Text roomName; //*
    public Text playerCount; //*

    public GameObject playerListing; //*
    public Transform playerListContent; //*

    public Button startButton; //*

    public void Start()
    {
        lobbyPanel.SetActive(true); //*
        roomPanel.SetActive(false); //*
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createInput.text))
            return;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false); //*
        roomPanel.SetActive(true); //*

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList; //*

        playerCount.text = "" + players.Length;

        for (int i = 0; i < players.Length; i++) //*
        {
            Instantiate(playerListing, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(players[i]);

            if (i == 0)
            {
                startButton.interactable = true;
            }
            else
            {
                startButton.interactable = false;
            }
        }

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        error.text = message;
        Debug.Log("Error creating room! " + message);
    }

    public void OnClickLeaveRoom() //*
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //*
    {
        SceneManager.LoadScene("Loading");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //*
    {
        Instantiate(playerListing, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(newPlayer);
    }

    public void OnClickStartGame() //*
    {
        PhotonNetwork.LoadLevel("Arena");

    }

}
