using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTxt, damageTxt, defenseTxt, speedTxt, critRateTxt;

    public int health;
    public float damage;
    public float defense;
    public float speed;
    public float critRate;
  
    void Start()
    {
        DisplayStats();
    }

    public void DisplayStats()
    {
        healthTxt.text = "Health:     " + health.ToString();
        damageTxt.text = "Damage:     " + damage.ToString();
        defenseTxt.text = "Defense:     " + defense.ToString();
        speedTxt.text = "Speed:     " + speed.ToString();
        critRateTxt.text = "Critical:     " + critRate.ToString();
    }
  
}
