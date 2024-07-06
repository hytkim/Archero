using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region singletone
    private static CameraMovement single;
    public static CameraMovement Single
    {
        get
        {
            if (single == null)
            {
                single = FindObjectOfType<CameraMovement>();

                if (single == null)
                {
                    var instanceContainer = new GameObject("CameraMovement");
                    single = instanceContainer.AddComponent<CameraMovement>();
                }
            }
            return single;
        }
    }
    #endregion
    public GameObject Player;

    public float offsetY = 45f;
    public float offsetZ = -40f;

    Vector3 cameraPosition;

    private void LateUpdate()
    {
        cameraPosition.y = offsetY + Player.transform.position.y;
        cameraPosition.z = offsetZ + Player.transform.position.z;

        transform.position = cameraPosition;
    }

    public void CarmeraNextRoom()
    {
        cameraPosition.x = Player.transform.position.x;
    }
}
