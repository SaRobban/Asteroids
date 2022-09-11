using UnityEngine;
using System;
public class HUD_WorldControllDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;
    public Action<HUD_WorldControllDisplay> A_OnDestroy;
    public ShipComponent shipComponent { private set; get; }

    public void OnEnable()
    {
        if (text == null)
            text = GetComponent<TMPro.TMP_Text>();
    }
    private void OnDestroy()
    {
        A_OnDestroy?.Invoke(this);

        if (shipComponent == null)
            return;

        shipComponent.A_OnKeyBoundChanged -= SetKeyText;
        shipComponent.A_OnDestroy -= SelfDesturct;
        shipComponent.A_OnUpdated -= UpdatePos;
    }
    public void Setup(ShipComponent shipComponent)
    {
        this.shipComponent = shipComponent;

        SetKeyText(shipComponent.GetKeyBound());

        shipComponent.A_OnKeyBoundChanged += SetKeyText;
        shipComponent.A_OnDestroy += SelfDesturct;
        shipComponent.A_OnUpdated += UpdatePos;
    }

    public void SetKeyText(char key)
    {
        this.text.SetText(key.ToString());
    }
    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public void UpdatePos()
    {
        text.transform.position = shipComponent.transform.position - shipComponent.transform.TransformDirection(shipComponent.InputUIOffset);
        text.transform.rotation = shipComponent.transform.rotation;
    }


    public void SelfDesturct(ShipComponent shipComponent)
    {
        Destroy(this.gameObject);
    }
}
