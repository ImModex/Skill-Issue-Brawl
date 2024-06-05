using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class DamageCircle : MonoBehaviour
{
    private static DamageCircle instance;

    private Transform circleTransform;
    private Transform topTransform;
    private Transform bottomTransform;
    private Transform leftTransform;
    private Transform rightTransform;

    public float circleShrinkSpeed;

    private Vector3 circleSize;
    private Vector3 circlePosition;

    private Vector3 targetCircleSize;

    void Awake()
    {
        instance = this;
        
        circleShrinkSpeed = 0.8f;
       
        circleTransform = transform.Find("dz center");
        topTransform = transform.Find("dz top");
        leftTransform = transform.Find("dz left");
        rightTransform = transform.Find("dz right");
        bottomTransform = transform.Find("dz bottom");

        SetCircleSize(new Vector3(0,0), new Vector3(48, 48, 48));
        targetCircleSize = new Vector3(0, 0);
    }

    private void Update()
    {
        Vector3 sizeChangeVector = (targetCircleSize - circleSize).normalized;
        Vector3 newCircleSize = circleSize + sizeChangeVector * Time.deltaTime * circleShrinkSpeed;
        SetCircleSize(circlePosition, newCircleSize);
    }

   private void SetCircleSize(Vector3 position, Vector3 size)
   {
       circleTransform.localScale = size;
       circlePosition= circleTransform.localPosition;
       circleSize = size;

       topTransform.localPosition = new Vector3(0, 0,54-((48-size.y)/2));
       leftTransform.localPosition = new Vector3(-54 + ((48 - size.y) / 2),0);
       rightTransform.localPosition = new Vector3( 54 - ((48 - size.y) / 2),0);
       bottomTransform.localPosition = new Vector3(0, 0, -54 + ((48 - size.y) / 2));
    }

   private bool IsOutsideCircle(Vector3 position)
   {
       return Vector3.Distance(position, circlePosition) > circleSize.x * .5f;
   }

   //Static function to call from player (losing HP)
   public static bool IsOutsideCircle_Static(Vector3 position)
   {
       return instance.IsOutsideCircle(position);
   }
}
