using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс для формирования волн
public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner WS;

    [Header("Need Only for random spawn and Enemies Type > 0 (waves = 0)")]
    [SerializeField]
    string[] enemyTAGS;

    Coroutine CR;

    [SerializeField]
    [Header("Custom Wave Number (Set 0 for endless random spawn)")]
    CustomWave[] waves;

    public int waveNumber = 1;
    int wavesBias; // K значение в X + K, где X номер волны, количество врагов в волне спавнится от X До X + K в случае рандомного спавна если waves.Length == 0
    float bigWaveCD;

    private void Awake()
    {
        WS = this;
    }
    void Start()
    {
        wavesBias = ConfigLoader.LoadData().waveRandomFactor;
        bigWaveCD = ConfigLoader.LoadData().waveCD;
        CR = StartCoroutine(spawnWave());
    }

    //обновляем информацию о волне
    public void resetSpawner()
    {
        StopAllCoroutines();
        CancelInvoke();

        ObjectPool.ObjPool.disableObjectsInPool();

        waveNumber = 1;

        CR = StartCoroutine(spawnWave());

    }

    //вызывается рандомную волну, если не настроены кастомные волны в инспекторе
    IEnumerator spawnWave()
    {
        if (waves.Length != 0)
        {
            foreach (CustomWave w in waves)
            {
                StartCoroutine(w.spawnWave());
                yield return new WaitForSeconds(bigWaveCD);
            }
        }
        else
        {
            
            InvokeRepeating("spawnRandomWave", 1, bigWaveCD);
            yield return new WaitForEndOfFrame();
        }
  
    }

    //коллит корутину, нужен для InvokeRepeating
    void spawnRandomWave()
    {
        StartCoroutine(spawnRandomWaves());
    }

    public IEnumerator spawnRandomWaves()
    {
        for (var i = 0; i < WS.waveNumber + Random.Range(0, WS.wavesBias); i++)
        {
            string enemyTAG = WS.enemyTAGS[Random.Range(0, WS.enemyTAGS.Length)]; //берет все указанные в эдиторе теги противников
            GameObject enemy = ObjectPool.ObjPool.GetObjectFromPool(enemyTAG);
            if (enemy != null)
            {
                GameObject[] route = GameRoutes.instance.routes[Random.Range(0, GameRoutes.instance.routes.Length)].route; // берет случайный путь
                enemy.transform.position = route[0].transform.position;
                enemy.GetComponent<EnemiAIInterface>().assignBehavior(new EnemyMove(route, enemy)); //при активации назначает ему этот путь и поведение 
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(2);
        }
        WS.waveNumber++;
    }

    //то же самое что и рандом волна, но есть возможность разбить на маленькие волны, для генерации различных противников
    [System.Serializable]
    public class CustomWave
    {
        [SerializeField]
        public int subWavesCD;
        [SerializeField]
        public SubWave[] subwaves;
    

        public IEnumerator spawnWave()
        {
           foreach(SubWave sw in subwaves)
            {
                WS.StartCoroutine(sw.spawnSubWave());
                yield return new WaitForSeconds(subWavesCD);
                WS.waveNumber++;
            }
        }


        [System.Serializable]
        public class SubWave
        {
            [SerializeField]
            string enemyTAG;
            [SerializeField]
            float spawnInterval;
            [SerializeField]
            int maxEnemies;
            [SerializeField]
            int routeNumber;

            public IEnumerator spawnSubWave()
            {
                for (var i = 0; i < maxEnemies; i++)
                {
                    GameObject enemy = ObjectPool.ObjPool.GetObjectFromPool(enemyTAG);
                    if (enemy != null)
                    {
                        GameObject[] route = GameRoutes.instance.routes[routeNumber].route;
                        enemy.transform.position = route[0].transform.position;
                        enemy.GetComponent<EnemiAIInterface>().assignBehavior(new EnemyMove(route, enemy));
                        enemy.SetActive(true);
                    }
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
        }

    }

}
