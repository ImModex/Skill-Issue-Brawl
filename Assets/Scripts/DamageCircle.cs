using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class DamageCircle : MonoBehaviour
{
    private static DamageCircle instance;

    public Transform circleTransform;
    public Transform topTransform;
    public Transform bottomTransform;
    public Transform leftTransform;
    public Transform rightTransform;
    public ParticleSystem SmokeRing;

    public float circleShrinkSpeed;

    public bool Circleshrink = false;

    private Vector3 circleSize;

    private Vector3 targetCircleSize = Vector3.zero;
    private Vector3 sizeChangeVector = new Vector3(-0.8f,-0.8f,-0.8f);

    void Awake()
    {
        instance = this;
        
        SetCircleSize(new Vector3(48, 48, 48));
    }

    private void Update()
    {
        if(!Circleshrink)
        {
            return;
        }

        Vector3 newCircleSize = circleSize + sizeChangeVector * Time.deltaTime * circleShrinkSpeed;
        SetCircleSize(newCircleSize);
    }

   private void SetCircleSize(Vector3 size)
   {
        circleTransform.localScale = size;
        circleSize = size;

        var sh = SmokeRing.shape;
        sh.scale += sizeChangeVector * Time.deltaTime * circleShrinkSpeed;

        if(sh.scale.x < 15)
        {
            var em = SmokeRing.emission;
            em.enabled = false;
        }

        topTransform.localPosition = new Vector3(0, 0,54-((48-size.y)/2));
        leftTransform.localPosition = new Vector3(-54 + ((48 - size.y) / 2),0);
        rightTransform.localPosition = new Vector3( 54 - ((48 - size.y) / 2),0);
        bottomTransform.localPosition = new Vector3(0, 0, -54 + ((48 - size.y) / 2));
    }

   private bool IsOutsideCircle(Vector3 position)
   {
       return Vector3.Distance(position, Vector3.zero) > circleSize.x * .5f;
   }


   //Static function to call from player (losing HP)
   public static bool IsOutsideCircle_Static(Vector3 position)
   {
       return instance.IsOutsideCircle(position);
   }
}
