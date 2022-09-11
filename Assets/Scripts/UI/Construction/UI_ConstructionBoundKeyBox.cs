using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class  UI_ConstructionBoundKeyBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ShipComponent shipComponent;
    private UI_ConstructionManager manager;
    [SerializeField] private TMP_Text nameTag;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image backGround;

    public void Setup(ShipComponent shipComponent, UI_ConstructionManager manager)
    {
        this.shipComponent = shipComponent;
        this.manager = manager;
        nameTag.SetText(FilterName(shipComponent.transform.name));
        SetKeyWithoutNotify(shipComponent.GetKeyBound());

        shipComponent.A_OnKeyBoundChanged += SetKeyWithoutNotify;
        shipComponent.A_OnDestroy += SelfDestruct;
    }

    private void OnDestroy()
    {
        if (shipComponent == null)
            return;
        shipComponent.A_OnKeyBoundChanged -= SetKeyWithoutNotify;
        shipComponent.A_OnDestroy -= SelfDestruct;
    }
    public void SelfDestruct(ShipComponent shipComponent)
    {
        manager.RemoveBindBoxFromList(this);
        Destroy(this.gameObject);
    }

    private void SetKeyWithoutNotify(char key)
    {
        inputField.SetTextWithoutNotify(key.ToString());
    }

    public string FilterName(string name)
    {
        if (name.Contains("("))
        {
            int i = name.IndexOf("(");
            return name.Substring(0, i);
        }
        return name;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        shipComponent.SetOutLine(true);
        backGround.color = Color.red;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        shipComponent.SetOutLine(false);
        backGround.color = Color.black;
    }

    public void OnValueChange(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return;
        }
        char c = s[s.Length - 1];
        c = char.ToUpper(c);
        inputField.SetTextWithoutNotify(c.ToString());
        shipComponent.SetKeyBound(c);
    }
}