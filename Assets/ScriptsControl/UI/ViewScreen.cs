using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screen;

    public virtual void Show()
    {
        _screen.SetActive(true);
    }

    public virtual void Hide()
    {
        _screen.SetActive(false);
    }
}
