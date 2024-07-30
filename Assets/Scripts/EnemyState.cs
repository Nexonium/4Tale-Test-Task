
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Settings for image of enemy planned action above head
/// </summary>

public class EnemyState : MonoBehaviour
{

    public Sprite attack;
    public Sprite defence;
    public Sprite heal;

    public Image imageComponent;

    public ActionImage actionImage;

    public enum ActionImage
    {
        None,
        Attack,
        Defence,
        Heal
    }

    private void OnValidate()
    {
        switch(actionImage)
        {
            case ActionImage.None:
                imageComponent.sprite = null;
                SetVisible(false);
                break;
            case ActionImage.Attack:
                imageComponent.sprite = attack;
                SetVisible(true);
                break;
            case ActionImage.Defence:
                imageComponent.sprite = defence;
                SetVisible(true);
                break;
            case ActionImage.Heal:
                imageComponent.sprite = heal;
                SetVisible(true);
                break;
        }
    }

    public void Awake()
    {
        imageComponent = GetComponent<Image>();
        SetVisible(true);
    }

    public void ChangeStateImage(EnemyAction action)
    {
        switch(action.actionName)
        {
            case "Attack":
                imageComponent.sprite = attack;
                break;
            case "Defence":
                imageComponent.sprite = defence;
                break;
            case "Heal":
                imageComponent.sprite = heal;
                break;
        }
    }

    public void SetVisible(bool state)
    {
        imageComponent.enabled = state;
    }
}
