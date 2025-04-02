using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpOxygenManager : MonoBehaviour
{
    public static HpOxygenManager instance;
    private void Awake()
    {
        instance = this;
    }


    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider oxygenSlider;

    [SerializeField] private GameObject show;

    private float maxHp;
    private float maxOxygen;

    public float curHp;
    public float curOxygen;

    private void Start()
    {
        maxHp = 100f;
        curHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = hpSlider.maxValue;

        maxOxygen = 255f;
        curOxygen = maxOxygen;
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = oxygenSlider.maxValue;
    }

    private void Update()
    {
        hpSlider.value = curHp;
        oxygenSlider.value = curOxygen;

        UseOxygen();
    }

    public void OnHit(float damage)
    {
        Camera.instance.Shake(1f, 15);
        curHp -= damage;
    }

    private void UseOxygen()
    {
        curOxygen -= Time.deltaTime;
        //show.GetComponent<Image>().color = new Color(0, 0, 0, 255f - curOxygen);
    }

    public void GameOver()
    {
        if (curHp <= 0 || curOxygen <= 0)
        {
            print("Game Over!");
        }
    }
}
