using UnityEngine;
using UnityEngine.UI;
 
public class HealthBar : MonoBehaviour
{
    public Image HealthBarImage;
    private float health;
    public void SetHealthBarValue(float value)
    {
        // Debug.Log(health);
        health = value;
        HealthBarImage.transform.localScale = new Vector3(value, 1, 1);
        if(health <= 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if(health <= 0.6f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public bool DecreaseHealth(){
        if (health > 0.1f){
            SetHealthBarValue(GetHealthBarValue() - 0.2f);
            return health < 0.1f ? false : true;
        }
        return false;
    }
    public float GetHealthBarValue()
    {
        return health;
    }
 
    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    public void Start(){
        SetHealthBarValue(1.0f);
    }
}