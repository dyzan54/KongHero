using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinItem : MonoBehaviour
{
    [SerializeField] AudioSource collectClip;
    private int coinValue = 10;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collectClip.Play();
            UpdateCoinCollect();
            Invoke("DestroyCoin", 0.1f);

        }
    }
    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

    public void UpdateCoinCollect()
    {
        CoinUIController.Instance.SetCoin(coinValue);
    }




}
