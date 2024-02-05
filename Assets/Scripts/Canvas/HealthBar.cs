using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerAtributes _attributes;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _attributes = GameObject.Find("Knight").GetComponent<PlayerAtributes>();
    }

    public void UpdateFill()
    {
        float healthBarFill = (float)_attributes.health / _attributes.maxHealth;
        _image.fillAmount = healthBarFill;
    }
}
