using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class multipleimagetracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    List<GameObject> objs = new List<GameObject>();
    
// Start is called before the first frame update
    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        //ARTrackedImageManager 컴포넌트를 가져와서
        imageManager.trackedImagesChanged += OnImageTrackedEvent;
        //imageManager컴포넌트에 trackedImagesChanged 이벤트에 OnImageTrackedEvent 함수를 더해
        //이벤트 조건이 충족될시 OnImageTrackedEvent함수를 실행시킨다
        //이벤트 조건 (정해진 이미지 인식)
    }

    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        //인식한 이미지 목록 전부를 조사해
        foreach (ARTrackedImage trackedImage in arg.added)
        {
            //이미지 하나당 이미지의 ReferenceImage 라이브러리에 등록된 이름을 imageName이라는 변수에 초기화
            string imageName = trackedImage.referenceImage.name;

            //위에서 초기화한 imageName이름의 리소시스 폴더 안에서 가져와서 prefab 오브젝트로 초기화
            GameObject prefab = Resources.Load<GameObject>(imageName);
            
            //만약 리소시스 안에 imageName 라는 이름의 오브젝트가 존재한다면 생성
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.name = imageName;
                obj.transform.SetParent(trackedImage.transform);
                objs.Add(obj);
   
            }
            
            //※주의
            //ReferenceImageLibrary(사진 넣는 곳)에 Name 와 리소시스 폴더 안에있는 오브젝트 이름이 같아야함
            //Keep Texture at Runtime 체크 (중요)
            //Specify Size 체크후
            //Physical Size 입력
            //0.01당 실제 1cm 정도의 비율
        }
    }
}
