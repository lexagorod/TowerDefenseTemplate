using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// данные игрока, работа с ними и связанное с этим UI
public class PlayerData : MonoBehaviour
{
    public static PlayerData playerData;

    public int initLives;
    public int initGold;
    private int deadEnemies;
   

    private int _lives;
    private int _gold;

    public int lives { get { return _lives; } set { _lives = lives; } }
    public int gold { get { return _gold; } set { _gold = gold; } }

    void Awake()
    {
        playerData = this;
    }

    private void Start()
    {
        _lives = initLives;
        _gold = initGold;

        RefreshUI();

    }

    private void Update()
    {
        CheckGameOver();
    }


    public ELEMENTS elementsUI;

    // класс для удержания UI компонентов
    [System.Serializable]
    public class ELEMENTS
    {
        public TextMeshProUGUI lives;
        public TextMeshProUGUI gold;
        public TextMeshProUGUI killedEnemies;
        public GameObject gameOverScreen;

    } 
    public TextMeshProUGUI livesTMP { get { return elementsUI.lives; } }
    public TextMeshProUGUI goldTMP { get { return elementsUI.gold; } }
    public GameObject gameOver { get { return elementsUI.gameOverScreen; } } // UI панель отображающая конец игры
    public TextMeshProUGUI killedEnemies { get { return elementsUI.killedEnemies; } } // количество убитых противников

    void RefreshUI()
    {
        elementsUI.lives.text = _lives.ToString();
        elementsUI.gold.text = _gold.ToString();
    }

    public void DamageToLives(int damage)
    {
        _lives -= damage;

        RefreshUI();
    }
    public void GoldSpent(int gold)
    {
        _gold -= gold;

        RefreshUI();
    }

    public void BountyGold(int gold)
    {
        deadEnemies++;
        _gold += gold;

        RefreshUI();
    }

    //проверка на конец игры, если жизней меньше 0
    void CheckGameOver()
    {
        if (_lives <= 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
            killedEnemies.text = "Killed Enemies: " + deadEnemies;

            ResetGame();
          
        }
    }

    //игра заново, если нажать любую кнопку
    void ResetGame()
    {
        if (Input.anyKey)
        {
            deadEnemies = 0;
            WaveSpawner.WS.ResetSpawner();
            gameOver.SetActive(false);
            Time.timeScale = 1;
            _lives = initLives;
            _gold = initGold;

            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Tower"))
            {
                gO.GetComponent<TowerData>().Reset();
            }

            RefreshUI();
        }
    }

}
