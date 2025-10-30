using System;
using System.Collections;
using UnityEngine;

public abstract class decision_component: MonoBehaviour
{
    protected const int WALK = 1;
    protected const int WORK = 2;
    protected const int RELAX = 3;
    protected const int EAT = 4;
    protected const int SOCIALIZE = 5;
    protected const int FIGHT = 6;
    protected const int SLEEP = 7;

    protected const int SECONDS_PER_MINUTE = 40;

    protected const int MAXIMUM_EVENT_STACK = 15;


    protected readonly Tuple<int, int> WALK_DURATION_RANGE = new Tuple<int, int>(SECONDS_PER_MINUTE * 200, SECONDS_PER_MINUTE * 2000);
    protected readonly Tuple<int, int> WORK_DURATION_RANGE = new Tuple<int, int>(600, 1200);
    protected readonly Tuple<int, int> SOCIALIZE_DURATION_RANGE = new Tuple<int, int>(1000, 20000);
    protected readonly Tuple<int, int> REST_DURATION_RANGE = new Tuple<int, int>(1500, 20000);
    protected readonly Tuple<int, int> EAT_DURATION_RANGE = new Tuple<int, int>(500, 2000);
    protected readonly Tuple<int, int> SLEEP_DURATION_RANGE = new Tuple<int, int>(1000, 2500);

    protected int current_event_duration;

    protected NPC npcScript;
    protected abstract void enable_behavior_pattern();
    protected abstract void decide_behavior_pattern();
    protected abstract void deactivate_behavior_pattern();
    protected abstract void reactivate_behavior_pattern();

    /*


    private int[] events_stack = new int[MAXIMUM_EVENT_STACK];
    private int current_event_index; 

    private int currentObjective;
    private float objective_duration;
    private bool objective_started;

    private UnityEngine.AI.NavMeshAgent agent;
    private walking_component walk;

    [SerializeField] private int objectivesNr;
    private time_script time;

    private void refreshObjectives()
    {

        for (int i = 0; i < objectivesNr; i++)
        {
            events_stack[i] = WALK;
        }
    }
    private void fulfillObjective()
    {
        if (current_event_index == objectivesNr - 1)
        {
            current_event_index = 0;
            refreshObjectives();
            return;
        }
        current_event_index++;
        currentObjective = events_stack[current_event_index];

        switch (currentObjective)
        {
            case WALK:
                objective_started = true;
                walk.choose_randomly = true;
                objective_duration = UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1,WALK_DURATION_RANGE.Item2);
                break;
            case WORK:
                objective_started = false;
                walk.choose_randomly = false;
                objective_duration = UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1, WALK_DURATION_RANGE.Item2);
                break;
            case EAT:
                objective_started= false;
                walk.choose_randomly = false;
                objective_duration = UnityEngine.Random.Range(EAT_DURATION_RANGE.Item1, EAT_DURATION_RANGE.Item2);
                break;
            case SOCIALIZE:
                objective_started= false;
                walk.choose_randomly = false;
                objective_duration = UnityEngine.Random.Range(SOCIALIZE_DURATION_RANGE.Item1,SOCIALIZE_DURATION_RANGE.Item2);
                break;            
            case SLEEP:
                objective_started = false;
                walk.choose_randomly = false;
                objective_duration = UnityEngine.Random.Range(SLEEP_DURATION_RANGE.Item1,SLEEP_DURATION_RANGE.Item2);
                break;
            case FIGHT:
                walk.choose_randomly = false;
                break;
                
         StartCoroutine(durationTimer());
        }
    }
    public IEnumerator durationTimer()
    {
        yield return new WaitForSeconds(objective_duration);
        fulfillObjective();
    }
    void Awake()
    {
        current_event_index = 0;
        time = GetComponent<time_script>();
        walk = GetComponent<walking_component>();
        fulfillObjective();
    }
    void FixedUpdate()
    {
        //walk.choose_randomly = true;
        if(objective_started)
            objective_duration -= Time.deltaTime;
        
    }
    */

}
