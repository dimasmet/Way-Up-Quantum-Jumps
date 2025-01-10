using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBalance : MonoBehaviour
{
    public static CoinBalance Instance;

    [SerializeField] private Text _balanceCoinText;
    [SerializeField] private Text _balanceCoinTextInShop;
    [SerializeField] private Text _coinValueOnGameText;

    private int countOnGame;
    private int balanceValue;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        EventsBusMini.OnStartLevel += StartLevel;

        balanceValue = PlayerPrefs.GetInt("Balance");

        UpdateView();
    }

    private void OnDisable()
    {
        EventsBusMini.OnStartLevel -= StartLevel;
    }

    private void UpdateView()
    {
        _balanceCoinText.text = balanceValue.ToString();
        _balanceCoinTextInShop.text = balanceValue.ToString();
        PlayerPrefs.SetInt("Balance", balanceValue);
    }

    public void AddToBalance(int reward)
    {
        balanceValue += reward;
        UpdateView();
    }

    public void DiscaseBalance(int value)
    {
        balanceValue -= value;
        UpdateView();
    }

    public int GetBalance()
    {
        return balanceValue;
    }

    private void StartLevel(int i)
    {
        countOnGame = 0;
        _coinValueOnGameText.text ="+ "  + countOnGame.ToString();
    }

    public void AddCoinInGame(int value)
    {
        countOnGame += value;
        _coinValueOnGameText.text = "+ " + countOnGame.ToString();

        HanSoundsGame.OnRunSound?.Invoke(HanSoundsGame.NameSound.CoinUp);
    }

    public int GetCoinResultInLevel()
    {
        return countOnGame;
    }
}
