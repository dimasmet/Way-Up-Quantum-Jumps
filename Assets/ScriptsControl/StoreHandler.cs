using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WrapShop
{
    public List<ItemShop> itemShops;
}

[System.Serializable]
public class ItemShop
{
    public int number;
    public int price;
    public StateItemShop stateItemShop;
}

public enum StateItemShop
{
    Close,
    Open,
    Active
}

[System.Serializable]
public class DataSkin
{
    public Sprite idleSprite;
    public Sprite moveSprite;
    public Sprite loseSprite;
}

public class StoreHandler : MonoBehaviour
{
    public static StoreHandler Instance;

    public WrapShop wrapShop;

    [SerializeField] private StoreView storeView;
    [SerializeField] private DataSkin[] dataSkins;

    public ShopButton currentButton;
    private int numberActive = 0;

    [SerializeField] private Player _player;

    [SerializeField] private Image _logoImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        string js = PlayerPrefs.GetString("JsShop");

        if (js != "")
        {
            wrapShop = JsonUtility.FromJson<WrapShop>(js);
        }

        storeView.InitShop(wrapShop);
        int number = PlayerPrefs.GetInt("NumberSkinLastActive");
        _player.SetSkinData(dataSkins[number]);
        _logoImage.sprite = dataSkins[number].idleSprite;
    }

    public void Save()
    {
        PlayerPrefs.SetString("JsShop", JsonUtility.ToJson(wrapShop));
        PlayerPrefs.SetInt("NumberSkinLastActive", numberActive);
    }

    public void ClickOnBtnShop(ShopButton shopButton, int number)
    {
        if (wrapShop.itemShops[number].stateItemShop == StateItemShop.Open)
        {
            wrapShop.itemShops[numberActive].stateItemShop = StateItemShop.Open;
            currentButton.UpdateStateView();

            numberActive = number;
            wrapShop.itemShops[numberActive].stateItemShop = StateItemShop.Active;
            currentButton = shopButton;
            currentButton.UpdateStateView();

            _player.SetSkinData(dataSkins[numberActive]);

            _logoImage.sprite = dataSkins[numberActive].idleSprite;

            Save();
        }
        else
        {
            if (wrapShop.itemShops[number].stateItemShop == StateItemShop.Close)
            {
                if (CoinBalance.Instance.GetBalance() >= wrapShop.itemShops[number].price)
                {
                    CoinBalance.Instance.DiscaseBalance(wrapShop.itemShops[number].price);

                    wrapShop.itemShops[numberActive].stateItemShop = StateItemShop.Open;
                    currentButton.UpdateStateView();

                    numberActive = number;
                    wrapShop.itemShops[numberActive].stateItemShop = StateItemShop.Active;
                    currentButton = shopButton;
                    currentButton.UpdateStateView();

                    _player.SetSkinData(dataSkins[numberActive]);

                    _logoImage.sprite = dataSkins[numberActive].idleSprite;

                    Save();
                }
            }
        }
    }
}
