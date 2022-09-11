using UnityEngine;

public class SceneUIFollowText : MonoBehaviour
{
    TMPro.TMP_Text text;
    public Transform target;
    public Vector3 offsett;


    public void OnEnable()
    {
        if (text == null)
            text = GetComponent<TMPro.TMP_Text>();
    }

    public void SetTarget(Transform target, Vector3 offsett,char key)
    {
        this.target = target;
        this.offsett = offsett;
        SetText(key);
    }

    public void SetText(char text)
    {
        this.text.SetText(text.ToString());
    }

    void Update()
    {
        if (target == null)
            return;

        text.transform.position = target.position - target.TransformDirection( offsett);
        text.transform.rotation = target.rotation;
    }
}
