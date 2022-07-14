using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIController : MonoBehaviour
{
    public static CoinUIController Instance;
    [SerializeField] public Text coinAmounts;
    int score;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void SetCoin(int coinValue)
    {
        score += coinValue;
        coinAmounts.text = "x" + score.ToString();
    }
    
}
