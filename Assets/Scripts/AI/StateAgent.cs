using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent
{
    [SerializeField] public Perception perception;
    public PathFollower path = null;
    public StateMachine stateMachine = new StateMachine();

    public BoolRef enemySeen;
    public BoolRef atDestination;
    public FloatRef enemyDistance;
    public FloatRef health;
    public FloatRef timer;

    public GameObject enemy { get; set; }

	private void Start()
	{

        stateMachine.AddState(new IdleState(this, typeof(IdleState).Name));
        stateMachine.AddState(new PatrolState(this, typeof(PatrolState).Name));
        stateMachine.AddState(new ChaseState(this, typeof(ChaseState).Name));
        stateMachine.AddState(new AttackState(this, typeof(AttackState).Name));
        stateMachine.AddState(new DeathState(this, typeof(DeathState).Name));

        //from first state to second
        stateMachine.AddTransition(typeof(IdleState).Name, new Transition(new Condition[] { new BoolCondition(enemySeen, true)}), typeof(ChaseState).Name);
        stateMachine.AddTransition(typeof(IdleState).Name, new Transition(new Condition[] { new FloatCondition(timer, Condition.Predicate.LESS, 0) }), typeof(PatrolState).Name);
        //stateMachine.AddTransition(typeof(IdleState).Name, new Transition(new Condition[] { new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0) }), typeof(DeathState).Name);
        stateMachine.AddTransition(typeof(IdleState).Name, new Transition(new Condition[] { new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0) }), typeof(DeathState).Name);

        stateMachine.AddTransition(typeof(PatrolState).Name, new Transition(new Condition[] { new BoolCondition(enemySeen, true) }), typeof(ChaseState).Name);
        stateMachine.AddTransition(typeof(PatrolState).Name, new Transition(new Condition[] { new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0) }), typeof(DeathState).Name);

        stateMachine.AddTransition(typeof(ChaseState).Name, new Transition(new Condition[] { new BoolCondition(enemySeen, false) }), typeof(IdleState).Name);
        stateMachine.AddTransition(typeof(ChaseState).Name, new Transition(new Condition[] { new FloatCondition(enemyDistance, Condition.Predicate.LESS_EQUAL, 2) }), typeof(AttackState).Name);
        stateMachine.AddTransition(typeof(ChaseState).Name, new Transition(new Condition[] { new FloatCondition(health, Condition.Predicate.LESS_EQUAL, 0) }), typeof(DeathState).Name);

        stateMachine.AddTransition(typeof(AttackState).Name, new Transition(new Condition[] { new FloatCondition(timer, Condition.Predicate.LESS_EQUAL, 0) }), typeof(ChaseState).Name);
        //attack to death state

        stateMachine.SetState(stateMachine.StateFromName(typeof(PatrolState).Name));

        //goes to death state when unity death event is triggered
        transform.GetComponent<Health>().OnDeath.AddListener(() => stateMachine.SetState(stateMachine.StateFromName(typeof(DeathState).Name)));
    }

	void Update()
    {
        var enemies = perception.GetGameObjects();
        enemySeen.value = (enemies.Length) != 0;
        enemy = (enemies.Length != 0) ? enemies[0] : null;
        enemyDistance.value = (enemy != null) ? (Vector3.Distance(transform.position, enemy.transform.position)) : float.MaxValue;
        timer.value -= Time.deltaTime;

        stateMachine.Update();
        animator.SetFloat("Speed", movement.velocity.magnitude);
    }

	/*private void OnGUI()
	{
		Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);

		GUI.Label(new Rect(screen.x, Screen.height - screen.y, 900, 60), stateMachine.GetStateName());
	}*/
}
