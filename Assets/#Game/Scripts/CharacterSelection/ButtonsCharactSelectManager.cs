using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ButtonsCharactSelectManager : MonoBehaviour
{
    //Pra dar bom, a ordem de associação dos objetos via unity precisam estar na mesma ordem.
    [SerializeField] private Button[] characterSelectButtons;
    [SerializeField] private GameObject CharacterDefault;   
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private Character[] Characters;

    private ExitGames.Client.Photon.Hashtable HashProperty = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        PhotonNetwork.LocalPlayer.TagObject = CharacterDefault.name;
        //define o player 0 como padrão. Daí o jogador pode escolher outro enquanto houver tempo.
        SetPlayerProperties(0);
    }


    public void ChooseCharacter(GameObject characterPrefab)
    {
        PhotonNetwork.LocalPlayer.TagObject = characterPrefab.name;
        SwitchButtonsInteractable(false);
    }

    //AÇÃO DOS BOTÕES DE SEÇÃO DE PERSONAGENS
    public void SwitchingIcon(int indexImgIcon)
    {
        SetPlayerProperties(indexImgIcon);
    }


    private void SetPlayerProperties(int indexCharacterbutton)
    {
        HashProperty["HP"] = Characters[indexCharacterbutton].HP;
        HashProperty["maxHP"] = Characters[indexCharacterbutton].HP;
        //HashProperty["damage"] = Characters[indexPlayer].damage;
        //HashProperty["ammo"] = Characters[indexPlayer].ammo;
        //HashProperty["maxAmmo"] = Characters[indexPlayer].ammo;
        //HashProperty["characterName"] = Characters[indexPlayer].characterName;
        HashProperty["characterIndex"] = Characters[indexCharacterbutton].characterIndex;
        
        HashProperty["timerRespawn"] = RoomConfigs.instance.timeToRespawn;
        HashProperty["isDead"] = false;

        PhotonNetwork.LocalPlayer.SetCustomProperties(HashProperty);

    }


    public void SwitchButtonsInteractable(bool boolean)
    {
        foreach (Button buttons in characterSelectButtons)
        {
            buttons.interactable = boolean;
        }

        cancelButton.SetActive(!boolean);
    }

}

