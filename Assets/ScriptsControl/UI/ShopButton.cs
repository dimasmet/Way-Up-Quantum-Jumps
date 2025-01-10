using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Text _stateText;
    [SerializeField] private Image _coinImg;

    private ItemShop itemShop;

    private void Awake()
    {
        _btn.onClick.AddListener(() =>
        {
            StoreHandler.Instance.ClickOnBtnShop(this, itemShop.number);
        });
    }

    public void InitSet(ItemShop itemShop)
    {
        this.itemShop = itemShop;
        if (itemShop.stateItemShop == StateItemShop.Active)
        {
            StoreHandler.Instance.ClickOnBtnShop(this, itemShop.number);
        }
        UpdateStateView();
    }

    public void UpdateStateView()
    {
        switch (itemShop.stateItemShop)
        {
            case StateItemShop.Close:
                _coinImg.gameObject.SetActive(true);
                _stateText.gameObject.SetActive(true);

                _stateText.text = itemShop.price.ToString();
                break;
            case StateItemShop.Open:
                _coinImg.gameObject.SetActive(false);
                _stateText.gameObject.SetActive(true);

                _stateText.text = "CHOOSE";
                break;
            case StateItemShop.Active:
                _coinImg.gameObject.SetActive(false);
                _stateText.gameObject.SetActive(true);

                _stateText.text = "ACTIVE";
                break;
        }
    }
}
