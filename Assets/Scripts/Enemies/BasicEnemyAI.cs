using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//основной класс работающий в информацией о враге
public class BasicEnemyAI : MonoBehaviour, EnemiAIInterface
{ 
    
    StateEnemy currentState; // текущий State Machine Behavior в котором находится враГ

    Animator anim;

    
    EnemyMove enemyMove; // Move компонент противника

    HealthBar healthBar;

    
    [SerializeField]
    Enemy enemyData; //вся информация о враге

    private void Awake()
    {
        enemyData.setMaxHealthValue(); // запоминается максимальный размер здоровья, установленный в эдиторе
    }
    void Update()
    {
        if (enemyMove != null)
        {
            currentState = currentState.Process(); //вызывается основной метод отвечающий за State Machine Behavior
            refreshStatus();
        }
    }

    //колится на каждого врага через WaveSpawner
    public void assignBehavior(EnemyMove EM)
    {
        this.enemyMove = EM;

        enemyData.resetHP();

        enemyData.getStronger();

        anim = GetComponent<Animator>();
        healthBar = transform.GetChild(1).GetComponent<HealthBar>();

        healthBar.maxHealth = enemyData.health;

        //для даннго типа врага изначально передается State Walk
        currentState = new Walk(this.gameObject, this.enemyMove, anim, enemyData);
    }

    public void refreshStatus()
    {
        healthBar.currentHealth = enemyData.health;
        if (healthBar.currentHealth <= 0)
        {
            gameObject.SetActive(false);
            PlayerData.playerData.bountyGold(enemyData.bounty);
        }
    }

    //враг дошел до точки, отправляется его в пулл к объектам и наносит урон жизням игрока на значение присвоенное врагу [enemyData.damage]
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndZone")
        {
            gameObject.SetActive(false);
            PlayerData.playerData.damageToLives(enemyData.damage);
        }


    }

    EnemyMove EnemiAIInterface.getMoveComponent()
    {
        return enemyMove;
    }

    public Enemy getEnemyData()
    {
        return enemyData;
    }
}
