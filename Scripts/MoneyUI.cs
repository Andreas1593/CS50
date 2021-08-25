using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{

    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        // Update current balance
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }

}
