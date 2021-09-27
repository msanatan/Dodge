using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUIManager : MonoBehaviour
{
    [SerializeField] GameObject[] mobileUI;
    [SerializeField] bool showInEditor = true;

    // Start is called before the first frame update
    void Start()
    {
        bool isMobile = SystemInfo.deviceType == DeviceType.Handheld;
        if (!isMobile && !(showInEditor && Application.isEditor)) {
            foreach (GameObject ui in mobileUI) {
                ui.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
