using Assets.Scripts.Passive;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text title;
    public Text description;
    public Image spriteImage;

    public void Show(Passive passive)
    {
        title.text = passive.name;
        description.text = passive.description;

        spriteImage.sprite = passive.sprite;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
