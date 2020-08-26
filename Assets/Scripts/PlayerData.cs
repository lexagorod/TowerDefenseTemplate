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

        refreshUI();

    }

    private void Update()
    {
        checkGameOver();
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

    void refreshUI()
    {
        elementsUI.lives.text = _lives.ToString();
        elementsUI.gold.text = _gold.ToString();
    }

    public void damageToLives(int damage)
    {
        _lives -= damage;

        refreshUI();
    }
    public void goldSpent(int gold)
    {
        _gold -= gold;

        refreshUI();
    }

    public void bountyGold(int gold)
    {
        deadEnemies++;
        _gold += gold;

        refreshUI();
    }

    //проверка на конец игры, если жизней меньше 0
    void checkGameOver()
    {
        if (_lives <= 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
            killedEnemies.text = "Killed Enemies: " + deadEnemies;

            resetGame();
          
        }
    }

    //игра заново, если нажать любую кнопку
    void resetGame()
    {
        if (Input.anyKey)
        {
            deadEnemies = 0;
            WaveSpawner.WS.resetSpawner();
            gameOver.SetActive(false);
            Time.timeScale = 1;
            _lives = initLives;
            _gold = initGold;

            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Tower"))
            {
                gO.GetComponent<TowerData>().reset();
            }

            refreshUI();
        }
    }

}
