using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUIProps : MonoBehaviour
{
    [SerializeField] private Image[] orderedHabilIcon;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;

    private int characterIndex;

    private ExitGames.Client.Photon.Hashtable HashProperty = new ExitGames.Client.Photon.Hashtable();


    private void Start()
    {
        //int n = Int32.Parse(UnityEngine.Random.Range(0, 6).ToString());
        PhotonNetwork.LocalPlayer.TagObject = RoomConfigs.instance.charactersOrdered[0].characterPrefab.name;

    }

    private void SetUICharProps(int characterIndex)
    {
        this.characterIndex = characterIndex;
        Character character = RoomConfigs.instance.charactersOrdered[characterIndex];
        
        characterName.text = character.name;
        characterDescription.text = character.Descricao;

        for(int i= 0; i < orderedHabilIcon.Length; i++)
        {
            orderedHabilIcon[i].sprite = character.ordenedHabillityIcon[i];
        }

    }


    private void SetPlayerProperties(int indexCharacter)
    {

        HashProperty["HP"] = RoomConfigs.instance.charactersOrdered[indexCharacter].HP;
        HashProperty["maxHP"] = RoomConfigs.instance.charactersOrdered[indexCharacter].HP;
        HashProperty["characterName"] = RoomConfigs.instance.charactersOrdered[indexCharacter].characterName;
        HashProperty["characterIndex"] = RoomConfigs.instance.charactersOrdered[indexCharacter].characterIndex;
        HashProperty["killCount"] = 0;
        HashProperty["deathCount"] = 0;
        HashProperty["timerRespawn"] = RoomConfigs.instance.timeToRespawn;
        HashProperty["isDead"] = false;

        PhotonNetwork.LocalPlayer.SetCustomProperties(HashProperty);

    }

    public void SetUIProps(int characterIndex)
    {
        HabillityButton[] h = FindObjectsOfType<HabillityButton>();
        foreach (HabillityButton habillity in h)
        {
            habillity.characterIndex = characterIndex;

        }

        SetUICharProps(characterIndex);
        SetPlayerProperties(characterIndex);
    }

    public void ChooseCharacter()
    {

        PhotonNetwork.LocalPlayer.TagObject = RoomConfigs.instance.charactersOrdered[characterIndex].characterPrefab.name;
        audioCharacterSceneController.instance.audioPlayerVoiceLines("characterSelected", characterIndex);

    }

}
