using UnityEngine;
using UnityEngine.UI;
 
public class HealthBar : MonoBehaviour
{
    public Image HealthBarImage;
    public void SetHealthBarValue(float value)
    {
        HealthBarImage.transform.localScale = new Vector3(value, 1, 1);
        if(HealthBarImage.fillAmount < 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if(HealthBarImage.fillAmount < 0.6f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }
 
    public float GetHealthBarValue()
    {
        return HealthBarImage.transform.localScale.z;
    }
 
    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    public void Start(){
        SetHealthBarValue(0.7f);
    }
}