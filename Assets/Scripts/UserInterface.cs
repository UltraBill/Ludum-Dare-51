using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Slider HealthSlider;

    public BaseCharacter character;

    private void Update()
    {
        HealthSlider.maxValue = character.GetMaxLifePoint();
    }
}
