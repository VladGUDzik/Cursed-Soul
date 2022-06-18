using UnityEngine;
using UnityEngine.UI;

namespace Health.MainHeart
{
    public class HealthComponent : MonoBehaviour
    {
        [field: SerializeField] public float maxHealth { get; private set; }
        public static HealthComponent instanceHealthComponent { get; private set; }

        public float currentHealth { get;  set; }

       // public List<GameObject> maxHealth = new List<GameObject>();
        [SerializeField]private Slider slider;
        [SerializeField]private Gradient gradient;
        [SerializeField]private Image fill;
        
        private void Start() {
           maxHealth = slider.value;
           instanceHealthComponent = this;
        }

        public void SetMaxHealth(float health)
        {
            maxHealth = health;
            currentHealth = maxHealth;
            slider.value =maxHealth;
            fill.color= gradient.Evaluate(1f);
        }
        
        public void SetHealth(float health) {
            currentHealth = health;
            slider.value = currentHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        private void Update()
        {
            // if (maxHealth !=5)
            //     maxHealth[maxHealth].SetActive(false);
        }
    }
}

