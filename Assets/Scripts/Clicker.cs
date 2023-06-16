using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public static Clicker Instance;
    public GameObject ShopPanel;
    public GameObject SettingsPanel;
    public GameObject AchievementsPanel;

    public void ShowAndHideShop()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }
    public void ShowAndHideSettings()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
    }
    public void ShowAndHideAchievements()
    {
        AchievementsPanel.SetActive(!AchievementsPanel.activeSelf);
    }
    public float Money
    {
        get => PlayerPrefs.GetFloat("Money", 0);
        private set => PlayerPrefs.SetFloat("Money", value);
    }
    [SerializeField]
    private Text money;
    [SerializeField]
    private List<AmplifierPref> amplifierPrefs;

    private List<DamageAmplifier> amplifiers;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        amplifiers = new List<DamageAmplifier>()
        {
            new DamageAmplifier(DamageAmplifier.AmplifierType.PLUS_CLICK_DAMAGE, 0, false, 1f, 1, 100, 125),
            new DamageAmplifier(DamageAmplifier.AmplifierType.CLICK_CRIT, 100, false, 2, 2f, 200, 100,  85),
            new DamageAmplifier(DamageAmplifier.AmplifierType.PASSIVE_DAMAGE, 0, true, 2, 1, 125, 100)
        };

        for (int i = 0; i < amplifierPrefs.Count; i++)
            amplifierPrefs[i].SetData(amplifiers[i]);

        StartCoroutine(PassiveDamageDealer());
        UpdateUI();
    }

    private IEnumerator PassiveDamageDealer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float damage = GetPassiveDamage();
            DamageTarget(damage);
            EffectsController.Instance.CreatePassiveEffect((int)damage);
        }
    }
    public void Click()
    {
        float damage = GetClickDamage();
        DamageTarget(damage);
        EffectsController.Instance.CreateClickEffect((int)damage);
    }
    
    private void DamageTarget(float damage)
    {
        AddMoney(damage);
    }

    private float GetClickDamage()
    {
        float damage = 1;

        var sortedAmplifiers = amplifiers.FindAll(x => !x.IsPassive); //проверка на пассивку
        sortedAmplifiers.Sort((x, y) =>  x.Priority.CompareTo(y.Priority)); // сортировка по приоритету для крита

        foreach (var amplifier in sortedAmplifiers)
            damage = amplifier.CalculateDamage(damage);

        return damage;
    }

    private float GetPassiveDamage()
    {
        float damage = 0;

        var sortedAmplifiers = amplifiers.FindAll(x => x.IsPassive);
        sortedAmplifiers.Sort((x, y) => x.Priority.CompareTo(y.Priority));

        foreach (var amplifier in sortedAmplifiers)
            damage = amplifier.CalculateDamage(damage);

        return damage;
    }

    public void UpdateUI()
    {
        money.text = "$" + (int)Money;
    }

    public void AddMoney(float value)
    {
        Money += value;
        UpdateUI();

        foreach (var pref in amplifierPrefs)
            pref.UpdateUI();
    }
}



