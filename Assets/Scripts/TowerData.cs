using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// базовый класс для вышки
public class TowerData : MonoBehaviour
{

    private int minCost;
    private float minFireRate;
    private int initDamage;

    public int cost;
    public float fireRate;
    public int damage;
    PlayerData playerData;
    TextMeshProUGUI costTMP;

    void Start()
    {
        costTMP = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        minCost = cost;
        minFireRate = fireRate;
        initDamage = damage;
        costTMP.text = cost.ToString();
        playerData = PlayerData.playerData;

    }

    void OnMouseUp()
    {
        if (playerData.gold >= cost)
        {
            playerData.GoldSpent(cost);

            fireRate *= 0.8f;
            damage = damage + 1;
            cost *= 2;
            costTMP.text = cost.ToString();
            
        }
    }

    //в случае новой игры обновляет параметры вышки
    public void Reset()
    {
        cost = minCost;
        fireRate = minFireRate;
        damage = initDamage;
        costTMP.text = cost.ToString();
    }

}
