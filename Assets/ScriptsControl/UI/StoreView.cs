using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreView:ViewScreen
{
    [SerializeField] private ShopButton[] shopButtons;

    [SerializeField] private Button _backBtn;

    private void Awake()
    {
        _backBtn.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.MenuUI);
        });
    }

    public void InitShop(WrapShop wrapShop)
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            shopButtons[i].InitSet(wrapShop.itemShops[i]);
        }
    }

    public override void Show()
    {
        base.Show();
        UpdateView();
    }

    public void UpdateView()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            shopButtons[i].UpdateStateView();
        }
    }
}
