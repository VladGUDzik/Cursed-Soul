using UnityEngine.UI;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [field: SerializeField] public float MaxShield { get; private set; }
    public float CurrentShield { get;  set; }
    public static Shield InstanceShield { get; private set; }
    
    [SerializeField]private Slider slider;
    [SerializeField]private Gradient gradient;
    [SerializeField]private Image fill;

    private void Start()
    {
        MaxShield = slider.value;
        InstanceShield = this;
    }
    
    public void SetMaxShield(float shield)
    {
        MaxShield = shield;
        CurrentShield = MaxShield;
        slider.value = MaxShield;
        fill.color= gradient.Evaluate(1f);
    }
    public void SetShield(float shield)
    {
        CurrentShield = shield;
        slider.value = CurrentShield;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetBonusShield(float shield)
    {
        CurrentShield = shield;
        if (CurrentShield <= 0)
            CurrentShield = 1;
        if (CurrentShield > 100)
            CurrentShield = 100;
        slider.value = CurrentShield;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}
