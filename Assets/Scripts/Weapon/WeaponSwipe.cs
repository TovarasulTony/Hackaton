using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditorInternal;

public class WeaponSwipe : MonoBehaviour
{
    public void AttackSwipeFinished()
    {
        Destroy(gameObject);
    }
}
