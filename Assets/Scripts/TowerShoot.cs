using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//все что связано с атакой товера, интерфейс держит только метод shoot, потому что для различных башен возможен различный захват противников в цель
//компонент ложится на Shoot чайлд геймобджект башни
public class TowerShoot : MonoBehaviour, ITowerShoot
{
    public List<GameObject> enemiesInRange;

    private float lastShotTime;
    private TowerData towerData;
    Animator anim;

    static readonly int shootTrigger = Animator.StringToHash("shoot");

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInParent<TowerData>();
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;
   
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            //захватывается противник, которому ближе всего до ближайшей целевой точки
            float distanceToGoal = enemy.GetComponent<IEnemiAI>().getMoveComponent().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > towerData.fireRate)
            {
                shoot(target);
                lastShotTime = Time.time;
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IEnemiAI>() != null)
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IEnemiAI>() != null)
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }


        public void shoot(GameObject enemy)
    {
        anim.SetTrigger(shootTrigger);
        enemy.GetComponent<IEnemiAI>().getEnemyData().getDamage(towerData.damage);

    }
}
