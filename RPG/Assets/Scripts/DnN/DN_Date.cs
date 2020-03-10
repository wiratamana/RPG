using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DN_Date
{
    public readonly int Day;
    public readonly int Hour;
    public readonly int Minute;

    public DN_Date(int day, int hour, int minute)
    {
        Day = day;
        Hour = hour;
        Minute = minute;
    }

    public override string ToString()
    {
        return $"Day {Day} - {Hour.ToString("00")}:{Minute.ToString("00")}";
    }
}
