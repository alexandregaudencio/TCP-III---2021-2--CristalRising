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
    [SerializeField] private int CharacterDefaultIndex;   
    [SerializeField] private GameObject cancelButton;
    //[SerializeField] private Character[] Characters;

    private ExitGames.Client.Photon.Hashtable HashProperty = new ExitGames.Client.Photon.Hashtable();
    private void Start()
    {
        PhotonNetwork.LocalPlayer.TagObject = RoomConfigs.instance.charactersOrdered[CharacterDefaultIndex].characterPrefab.name;
        SetPlayerProperties(CharacterDefaultIndex);
    }



    //AÇÃO DOS BOTÕES DE SEÇÃO DE PERSONAGENS
    public void ChooseCharacter(int characterIndex)
    {
        PhotonNetwork.LocalPlayer.TagObject = RoomConfigs.instance.charactersOrdered[characterIndex].characterPrefab.name;
        string id = characterIndex.ToString();
        audioCharacterSceneController.instance.audioPlayerVoiceLines("characterSelected", characterIndex);

        SetPlayerProperties(characterIndex);
    }


    private void SetPlayerProperties(int indexCharacter)
    {
        
        HashProperty["HP"] = RoomConfigs.instance.charactersOrdered[indexCharacter].HP;
        HashProperty["maxHP"] = RoomConfigs.instance.charactersOrdered[indexCharacter].HP;
        //HashProperty["ammo"] = Characters[indexPlayer].ammo;
        HashProperty["characterName"] = RoomConfigs.instance.charactersOrdered[indexCharacter].characterName;
        HashProperty["characterIndex"] = RoomConfigs.instance.charactersOrdered[indexCharacter].characterIndex;
        HashProperty["killCount"] = 0;
        HashProperty["deathCount"] = 0;
        HashProperty["timerRespawn"] = RoomConfigs.instance.timeToRespawn;
        HashProperty["isDead"] = false;

        PhotonNetwork.LocalPlayer.SetCustomProperties(HashProperty);

    }

    //public void SwitchButtonsInteractable(bool boolean)
    //{
    //    foreach (Button buttons in characterSelectButtons)
    //    {
    //        buttons.interactable = boolean;
    //    }

    //    cancelButton.SetActive(!boolean);
    //}


}

