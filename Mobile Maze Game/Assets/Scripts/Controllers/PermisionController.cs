using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermisionController : MonoBehaviour
{
    [SerializeField]
    GuiManager guiManager;
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
        if (!PermissionGranted())
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
            Permission.RequestUserPermission(Permission.ExternalStorageWrite, callbacks);
        }
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
        guiManager.PermissionDanedMessage();
    }
}
