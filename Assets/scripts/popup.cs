using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public Image popupImage; // 팝업 이미지 게임오브젝트

    public void OnButtonClick()
    {
        // 팝업 이미지 활성화
        popupImage.gameObject.SetActive(false);
    }
}
