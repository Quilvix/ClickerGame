using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AmplifierPref : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text level;
    [SerializeField]
    private Text price;

    private DamageAmplifier amplifier;
    private CanvasGroup group;

    public void SetData(DamageAmplifier amplifier)
    {
        group = GetComponent<CanvasGroup>();
        this.amplifier = amplifier; 
    }

    public void UpdateUI() 
    {
        level.text = "x" + amplifier.Level;
        price.text = "$" + amplifier.Price;

        group.alpha = Clicker.Instance.Money >= amplifier.Price ? 1 : .5f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Clicker.Instance.Money < amplifier.Price)
            return;

        Clicker.Instance.AddMoney(-amplifier.Price);
        amplifier.LevelUP();
        UpdateUI();
    }
}
