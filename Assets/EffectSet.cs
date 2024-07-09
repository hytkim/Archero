using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : MonoBehaviour
{
    #region singletone
    private static EffectSet single;
    public static EffectSet Single
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<EffectSet>();

                if (single == null)
                {
                    var instanceContainer = new GameObject("EffectSet");
                    single = instanceContainer.AddComponent<EffectSet>();
                }
            }
            return single;
        }
    }
    #endregion

    [Header("# Monster")]
    public GameObject DuckAtkEffect;
    public GameObject DuckDmgEffect;
    [Header("# Player")]
    public GameObject PlayerAtkEffect;
    public GameObject PlayerDmgEffect;

}
