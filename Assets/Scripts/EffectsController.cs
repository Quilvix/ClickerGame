using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsController : MonoBehaviour
{
    public static EffectsController Instance;
    [SerializeField]
    private Effect effectPref;

    public void Awake()
    {
        Instance = this;
    }

    public void CreateClickEffect(int value)
    {
        var pref = Instantiate(effectPref, transform, false);
        pref.SetClickPosition(Input.mousePosition);
        pref.SetValue(value);
    }

    public void CreatePassiveEffect(int value)
    {
        var pref = Instantiate(effectPref, transform, false);
        pref.SetPassivePosition(new Vector2(1 * 2, 1 / 3));
        pref.SetValue(value);
    }
}
