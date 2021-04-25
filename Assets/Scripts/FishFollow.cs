using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFollow : Fish
{

    protected override void Move()
    {
        if(playerTransform == null){return;}
        Vector3 aimVector = playerTransform.position - transform.position;
    	transform.up = aimVector;

        transform.Translate(transform.up * speedMovement * Time.fixedDeltaTime, Space.World);
    }
}
