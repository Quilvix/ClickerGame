using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private CanvasGroup group;

    void Update()
    {
        group.alpha = Mathf.Lerp(group.alpha, 0, Time.deltaTime * 5);
        transform.position += Vector3.up * Time.deltaTime * 1;

        if (group.alpha < .01f)
            Destroy(gameObject);
    }
    public void SetClickPosition(Vector2 position)
    {
    }
    public void SetPassivePosition(Vector2 position)
    {
        transform.position = position;
    }

    public void SetValue(int value)
    {
        text.text = "+" + value;
    }
}
