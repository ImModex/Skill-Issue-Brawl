using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIconManager : MonoBehaviour
{
    public Image spell1;
    public Image spell2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImageOne(Sprite sprite)
    {
        spell1.sprite = sprite;
    }

    public void SetImageTwo(Sprite sprite)
    {
        spell2.sprite = sprite;
    }
}
