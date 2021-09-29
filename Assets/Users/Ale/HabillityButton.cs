﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HabillityButton : MonoBehaviour
{
    public int habillityIndex;
   [HideInInspector] public int characterIndex;

    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text characterDescriptText;

    public static HabillityButton instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

   
    public void UpdateUIProps()
    {
        Character character = RoomConfigs.instance.charactersOrdered[characterIndex];

        //icon
        GetComponentInChildren<Image>().sprite = character.ordenedHabillityIcon[habillityIndex];
        //Name
        characterNameText.text = character.name;
        characterDescriptText.text = character.Descricao;
        
        //nome da arma

        //descrição da arma
    }

}
