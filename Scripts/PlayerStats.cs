using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    // Required to reset money and lives since static variables carry on from one scene to another
    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }

}
