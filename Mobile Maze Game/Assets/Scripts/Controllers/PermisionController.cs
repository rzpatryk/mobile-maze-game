using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermisionController : MonoBehaviour
{

    public bool PermissionGranted()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CheckPermission()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Debug.Log("Granted");
        }
        else
        {
            bool useCallBacks = false;
            if (!useCallBacks)
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
            else
            {
                PermissionCallbacks callbacks = new PermissionCallbacks();
                callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                Permission.RequestUserPermission(Permission.ExternalStorageWrite, callbacks);
            }
        }
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
    }
}
