using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//интерфейс для всех "возможных" врагов

public interface IEnemiAI
{
    //метод берущий Move компонент и назначающий базовые параметры и State Machine Behaviour,
    //у разных врагов возможно будут разные параметры
    void AssignBehavior(EnemyMove EM);

    //метод для передачи здоровья на HealthBar
    //и проверки опустилось ли здоровье врага до 0
    void RefreshStatus();

    //метод для получения Move компонента
    EnemyMove GetMoveComponent();

    //метод для получения инстанса класса с информацией о враге
    Enemy GetEnemyData();
}
