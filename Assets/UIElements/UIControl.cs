using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Image fill;
    public Image baseFill;

    public float maxHealthPlayer;
    public float currentHealthPlayer;

    public float maxHealthBase;
    public float currentHealthBase;

    public Color fullHealthCol;
    public Color fullBaseCol;
    public Color minHealthCol;
    Text displayText;
    private string message;
    public int round;
    public int mag;
    public int currentReload;
    public int currentGun;
    private Color nonAlph;
    private Color alph;
    // Start is called before the first frame update
    void Start()
    {
        fill = GameObject.FindWithTag("HealthColor").GetComponent<Image>();

        displayText = GameObject.Find("AmmoText").GetComponent<Text>();

        GameObject.Find("Slider").GetComponent<Slider>().maxValue = maxHealthPlayer;
        GameObject.Find("BaseHealthSlide").GetComponent<Slider>().maxValue = maxHealthBase;

        nonAlph = new Color32(255,255,255,255);
        alph = new Color32(255,255,255,90);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthPlayer = GameObject.Find("Slider").GetComponent<Slider>().value;

        currentHealthBase = GameObject.Find("BaseHealthSlide").GetComponent<Slider>().value;

        baseFill.color = Color.Lerp(minHealthCol,fullBaseCol,(float)currentHealthBase/maxHealthBase);
        fill.color = Color.Lerp(minHealthCol,fullHealthCol,(float)currentHealthPlayer/maxHealthPlayer);

        message = "AMMO: " + currentReload.ToString() + "\\" + mag.ToString() 
                +  "\n\nROUND: " + round.ToString();
        
        displayText.text = message;

        if(currentGun == 0)
        {
            GameObject.Find("AutoRifle").GetComponent<RawImage>().color = nonAlph;
        }
        else
        {
            GameObject.Find("AutoRifle").GetComponent<RawImage>().color = alph ;
        }
        
        if(currentGun == 1)
        {
            GameObject.Find("ShotGun").GetComponent<RawImage>().color = nonAlph;
        }
        else
        {
            GameObject.Find("ShotGun").GetComponent<RawImage>().color = alph ;
        }


    }
}
