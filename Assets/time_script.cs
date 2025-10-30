using UnityEngine;

public class time_script : MonoBehaviour
{
    private int hour;
    private int minute;
    private float second;

    [SerializeField] private int seconds_per_minute;
    [SerializeField] private int minutes_per_hour;
    [SerializeField] private int hours_per_day;

    public int get_seconds_per_min()
    {
        return seconds_per_minute;
    }

    public int get_minutes_per_hour()
    {
        return minutes_per_hour;
    }
    public int get_hours_per_day()
    {
        return hours_per_day;
    }

    void Start()
    {
        hour = 0;
        minute = 0;
        second = 0;
    }

    void Update()
    {
        second += Time.deltaTime;
        if (second == seconds_per_minute)
        {
            minute += 1;
            if (minute == minutes_per_hour)
            {
                hour += 1;
                minute = 0;
                if (hour == hours_per_day)
                {
                    hour = 0;
                }
            }
        }
    }
}
