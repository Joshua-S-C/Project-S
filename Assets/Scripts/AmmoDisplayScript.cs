using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplayScript : MonoBehaviour
{
    public GameObject ammoText;
    public GameObject ammoBar;
    private float ammoBarMax;
    // Start is called before the first frame update
    void Start()
    {
        ammoBarMax = ammoBar.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateDisplay(int ammo,int maxAmmo,float ratio)
    {
        ammoText.GetComponent<TextMeshProUGUI>().text = ammo + "/" + maxAmmo;
        ammoBar.GetComponent<RectTransform>().sizeDelta = new Vector2(ammoBarMax * ratio, ammoBar.GetComponent<RectTransform>().sizeDelta.y);
    }
}
