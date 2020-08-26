using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State machine behaviour класс, в данный момент один противник с простым поведением. В случае нескольких противников с различным поведением дает возможность к расширению
public class StateEnemy
{
    // возможные стейты противника
    public enum STATE
    {
        WALK, RUN, SLOWED
    };

    // ивенты для захода и выхода из/в стейт
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }; 

    public STATE name; // название текущего стейта
    protected EVENT stage; // текущее состояние (заходит, находится или выходит из стейта)

    //различные компоненты
    protected GameObject npc; 
    protected EnemyMove moveAgent;
    protected Animator anim;
    protected StateEnemy nextState; // следующией стейт после текущего
    protected Enemy enemyData;
    protected int maxHP;

    // конструктор
    public StateEnemy(GameObject _npc, EnemyMove _moveAgent, Animator _anim, Enemy _enemyData)
    {
        npc = _npc;
        moveAgent = _moveAgent;
        anim = _anim;
        stage = EVENT.ENTER;
        enemyData = _enemyData;
        maxHP = enemyData.health;

    }

 
    public virtual void Enter() { stage = EVENT.UPDATE; } // в данный момент ENTER и EXIT пустые, но они довольно полезные для смены анимаций или каких то действий при входе или выходе
    public virtual void Update() { stage = EVENT.UPDATE; } // находишься в упдейте, пока не удовлитворишь условия, которые прописал для выхода из него внутри
    public virtual void Exit() { stage = EVENT.EXIT; } //

    // Вызывается другим классом, обычно с Monobehaviour компонентом в Update()
    public StateEnemy Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this; // если нет следующего стейта, то возвращаем этот же
    }
}

public class Walk : StateEnemy
{
    public Walk(GameObject _npc, EnemyMove _moveAgent, Animator _anim, Enemy _enemyData)
                : base(_npc, _moveAgent, _anim, _enemyData)
    {
        name = STATE.WALK; // Set name of current state
        moveAgent.speed = enemyData.speed; // How fast it moves 
        anim.speed = enemyData.speed;

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        //переходим в бег в случае когда хп меньше чем maxHP / 2
        moveAgent.Move();
        if (enemyData.health < maxHP / 2)
        {
            nextState = new Run(npc, moveAgent, anim, enemyData);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class Run : StateEnemy
{
   
    public Run(GameObject _npc, EnemyMove _moveAgent, Animator _anim, Enemy _enemyData)
                : base(_npc, _moveAgent, _anim, _enemyData)
    {

        name = STATE.RUN; // Set name of current state
        moveAgent.speed = enemyData.speed * 2; // How fast it moves 
        anim.speed = enemyData.speed * 2;

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        moveAgent.Move();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
