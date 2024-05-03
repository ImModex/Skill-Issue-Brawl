using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    public float moveSpeedMultiplyerChange;
    public float damageMultiplyerChange;
    public float durationSec;
    private GameObject caster;
    // Start is called before the first frame update
    void Start()
    {
        caster = transform.parent.gameObject;
        StartCoroutine(BuffCaster());
    }
 
    IEnumerator BuffCaster()
    {
        Statscript Stats = caster.gameObject.GetComponent<Statscript>();
        Stats.moveSpeedMultiplyer += moveSpeedMultiplyerChange;
        Stats.damageMultiplyer += damageMultiplyerChange;
        yield return new WaitForSeconds(durationSec);
        Stats.moveSpeedMultiplyer -= moveSpeedMultiplyerChange;
        Stats.damageMultiplyer -= damageMultiplyerChange;
        Destroy(gameObject);
    }
    
}
