using System;
using System.Collections.Generic;
using UnityEngine;

public class walker_pattern_node
{
    public npc_state crt;
    public int duration;
    public walker_pattern_node(npc_state crt, int duration){ this.crt = crt; this.duration = duration;}
}
//Simple node class
public class walker_decision_component : decision_component
{
    private List<List<walker_pattern_node>> behaviorPatterns = new List<List<walker_pattern_node>>();
    private List<walker_pattern_node> currentPattern = new List<walker_pattern_node>();
    private int activityIndex;
    private int patternIndex;
    private walker_pattern_node lastActivity;
    private float currentActivityTime; 
    protected override void decide_behavior_pattern()
    {
        if (patternIndex < behaviorPatterns[activityIndex].Count) return;
        patternIndex = UnityEngine.Random.Range(0,behaviorPatterns.Count-1);
        activityIndex = 0;
        enable_behavior_pattern();
    }
    protected override void enable_behavior_pattern()
    {
        if (patternIndex >= behaviorPatterns[activityIndex].Count)
        {
            decide_behavior_pattern();
            return;
        }
        switch (currentPattern[activityIndex].crt)
        {
            case npc_state.walking:
            {
                break;
            }
            case npc_state.resting:
            { 
                break; 
            }
            case npc_state.running:
            {
                break;
            }
            default:
                decide_behavior_pattern();
                break;

        }
    }
    protected override void deactivate_behavior_pattern()
    {

    }
    protected override void reactivate_behavior_pattern()
    {

    }
    protected void createPatterns()
    {
        for (int i = 0; i < 3; i++)
            behaviorPatterns.Add(new List<walker_pattern_node>());

        walker_pattern_node long_walk_start1 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1, WALK_DURATION_RANGE.Item2));
        walker_pattern_node short_walk_start1 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1, WALK_DURATION_RANGE.Item2));
        walker_pattern_node long_rest_start1 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(123,23232));
        //long_walk_start1 pattern construction
        behaviorPatterns[0].Add(long_walk_start1);
        walker_pattern_node short_rest1 = new walker_pattern_node(npc_state.resting, UnityEngine.Random.Range(REST_DURATION_RANGE.Item1, REST_DURATION_RANGE.Item2/2));
        behaviorPatterns[0].Add(short_rest1 ); 
        walker_pattern_node long_walk1_2 = new walker_pattern_node(npc_state.walking,UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1, WALK_DURATION_RANGE.Item2));
        behaviorPatterns[0].Add(long_walk1_2);
        walker_pattern_node long_rest1_2 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(REST_DURATION_RANGE.Item1, REST_DURATION_RANGE.Item2));
        behaviorPatterns[0].Add(long_rest1_2);
        walker_pattern_node long_walk1_3 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1, WALK_DURATION_RANGE.Item2));
        behaviorPatterns[0].Add(long_walk1_3);
        walker_pattern_node sleep_1_final = new walker_pattern_node(npc_state.sleeping, UnityEngine.Random.Range(SLEEP_DURATION_RANGE.Item1, SLEEP_DURATION_RANGE.Item2));
        behaviorPatterns[0].Add(sleep_1_final);
        //short_walk_start1 pattern construction
        behaviorPatterns[1].Add(short_walk_start1);
        walker_pattern_node long_walk2_1 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1, WALK_DURATION_RANGE.Item2*3));
        behaviorPatterns[1].Add(long_walk2_1);
        walker_pattern_node long_walk2_2 = new walker_pattern_node(npc_state.walking, UnityEngine.Random.Range(WALK_DURATION_RANGE.Item1*2, WALK_DURATION_RANGE.Item2*4));
        behaviorPatterns[1].Add(long_walk2_2);
        walker_pattern_node long_eat_2 = new walker_pattern_node(npc_state.eating, UnityEngine.Random.Range(EAT_DURATION_RANGE.Item1*2, EAT_DURATION_RANGE.Item2*5));
        behaviorPatterns[1].Add(long_eat_2);
        behaviorPatterns[1].Add(sleep_1_final);
        //long_rest_start1 pattern construction
    }
    void Awake()
    {
        npcScript = GetComponent<NPC>();

        createPatterns();
        decide_behavior_pattern();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentActivityTime = 120;
    }

    // Update is called once per frame
    void Update()
    {
        currentActivityTime -= Time.deltaTime;
        if (currentActivityTime == 50 || currentActivityTime <= 10)
            Debug.Log(currentActivityTime);
    }
}
