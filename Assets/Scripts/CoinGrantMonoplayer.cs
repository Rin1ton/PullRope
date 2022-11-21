using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinGrantMonoplayer
{
    public static bool playing;
    private static float Timer;
    private static float TimeChange;

    public static void RecordCurrentTime() // Records current time
    {
        Timer = Time.time;
    }

    public static void EndGame() // Calculates overall change in time and grants coins
    {
        TimeChange = Time.time - Timer;

        if (TimeChange <= 60f) // If less than 60 seconds
        {
            DatabaseManager.AdjustCoins(100); // Add 100 coins
        }
        else if (TimeChange <= 90f) // If less than 90 seconds
        {
            DatabaseManager.AdjustCoins(50);
        }
        else if (TimeChange <= 120f) // If less than 120 seconds
        {
            DatabaseManager.AdjustCoins(25);
        }
        else // If more than 120 seconds
        {
            DatabaseManager.AdjustCoins(10);
        }
    }
}
