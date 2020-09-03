using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//базовый класс для любого энеми с базовой информацией, наследуется в случае необходимости
[System.Serializable]
public class Enemy
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _bounty;
    [SerializeField]
    private int _speed;

    public int health { get { return _health; } set { _health = health; } }
    public int damage { get { return _damage; } set { _damage = damage; } }
    public int bounty { get { return _bounty; } set { _bounty = bounty; } }
    public int speed { get { return _speed; } set { _speed = speed; } }

    [HideInInspector]
    public int maxHP;

    public void SetMaxHealthValue()
    {
        maxHP = _health;
    }
    public void ResetHP()
    {
        _health = maxHP;

    }

    //увеличивает хп в зависимоти от волны поделенной на 2
    public void GetStronger()
    {
        int strongerIndex = WaveSpawner.WS.waveNumber/2;
        _health += Random.Range(strongerIndex, strongerIndex + 2);

    }

    public void GetDamage(int damage)
    {
        _health -= damage;
    }


}
