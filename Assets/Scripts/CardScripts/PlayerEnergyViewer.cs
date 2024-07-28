using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerEnergyViewer : MonoBehaviour
{

    private PlayerEntity player;
    private TMP_Text energyText;

    public UnityEvent<int> OnValueChanged;

    private void Awake()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        player = FindObjectOfType<PlayerEntity>();
        energyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        energyText.text = player.energy.ToString();
    }

}