using UnityEngine;
using ccU3DEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_Gallery : ccUILogicBase
    {
        private int imgIndex;
        //private ListPositionCtrl imgListCtrl;
        //private ListBox[] listBoxs;

        private bool[] img_Unlock;　//此處尚未完成，應傳入是否為已解鎖圖片的Bool陣列
        private bool bigImgOpened;

        //private Texture2D tBigImg;
        private Sprite[] bigImgSprites;
        private RectTransform _BigImgRectTrans;

        private Vector2 normalBigImgSize;
        private Vector3 normalBigImgPos;
        private Vector2 _CanvasSize;
        private Vector3 _CanvasPos;

        private Image _Img_BigImg;
        private GameObject _ConditionText;
        private Material lockedMaterialRef;

        protected override void On_Init()
        {
            int imgListBoxCount;
            bigImgOpened = false;

            GameObject _BigImg = f_GetObject("BigImg");
            _Img_BigImg = _BigImg.GetComponent<Image>();

            _BigImgRectTrans = _BigImg.GetComponent<RectTransform>();
            normalBigImgSize = _BigImgRectTrans.rect.size;  //取得大圖正常的大小
            normalBigImgPos = _BigImgRectTrans.localPosition;   //取得大圖正常的中心點

            Canvas _Canvas = ccU3DEngine.UIRoot.GetInstance().f_FindCanvas(UILayer.Window); 
            _CanvasSize = _Canvas.GetComponent<RectTransform>().rect.size;  //取得Canvas的大小
            _CanvasPos = _Canvas.GetComponent<RectTransform>().localPosition;   //取得Canvas的中心點

            //lockedMaterialRef = glo_Main.GetInstance().m_ResourceManager.f_CreateMateral("GrayScale");

            _ConditionText = _BigImg.transform.GetChild(0).gameObject;  //取得大圖子物件(關卡解鎖條件Text)
            f_RegClickEvent(_BigImg, On_BigImgClick, strEffectSound: "ButtonNormal");

            f_RegClickEvent(f_GetObject("ReturnBtn"), On_ReturnBtnClick, strEffectSound: "ButtonNormal");

            //imgListCtrl = f_GetObject("ImgList").GetComponent<ListPositionCtrl>();
            //imgListCtrl.Inital();   //初始化ImgList
            //imgListBoxCount = imgListCtrl.transform.childCount;
            ////img_Unlock = StaticValue._bGallery;
            //img_Unlock = new bool[imgListBoxCount];
            //listBoxs = new ListBox[imgListBoxCount];
            //bigImgSprites = new Sprite[imgListBoxCount];
            //Texture2D tempImgAsset;
            //for (int i = 0; i < imgListBoxCount; i++)   //註冊每個ListBox的物件相關並初始化
            //{
            //    listBoxs[i] = imgListCtrl.transform.GetChild(i).GetComponent<ListBox>();
            //    listBoxs[i].Initial();
            //    //imgListCtrl.transform.GetChild(i).GetComponent<Toggle>().onValueChanged.AddListener(BigImgChange);
            //    //listBoxs[i].GetComponent<Toggle>().onValueChanged.AddListener(BigImgChange);
            //    //ImgUnlock(img_Unlock[i], i);

            //    tempImgAsset = AssetLoader.LoadAsset("img_scene.bundle", "scene" + (i + 1)) as Texture2D;
            //    if(tempImgAsset != null)
            //        bigImgSprites[i] = Sprite.Create(tempImgAsset, new Rect(0, 0, tempImgAsset.width, tempImgAsset.height), Vector2.zero);

            //    int index = i;
            //    listBoxs[i].GetComponent<Toggle>().onValueChanged.AddListener(value => { SwitchBigImg(index); });
            //}
            //imgListCtrl.CenteringTargetBox(0, true);  //將Center移至[0]的ListBox
            //imgListCtrl.On_Update();
            //鍵盤才會用到
            /*eventSystem = GameMain.GetInstance().f_GetEventSystem();
            eventSystem.SetSelectedGameObject(rewardImg[0]);*/
            
            SwitchBigImg(0);

            f_RegClickEvent(f_GetObject("HelpBtn"), (go, _, __) => { ccUIManage.GetInstance().f_SendMsg("UIP_Help", BaseUIMessageDef.UI_OPEN, "Gallery"); }, strEffectSound: "ButtonNormal");
        }

        protected override void On_Open(object e)
        {
            ccUIManage.GetInstance().f_PlayBGM("BGMMerryGoRound");
            //eventSystem.firstSelectedGameObject = f_GetObject("StartBtn");
            Camera.main.orthographic = true;
            //img_Unlock = StaticValue._bGallery;
            //for (int i = 0; i < img_Unlock.Length; i++) ImgUnlock(img_Unlock[i], i);
            //SwitchBigImg(imgListCtrl.GetCenteredContentID());
        }

        protected override void On_Update()
        {
            //base.On_Update();

            //鍵盤功能
            /*if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(rewardImg[0]);
                //_startBtn.OnSelect(new BaseEventData(eventSystem));
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //if (eventSystem.currentSelectedGameObject == f_GetObject("StartBtn")) ccUIEventListener.Get(f_GetObject("StartBtn")).onClickV2.Invoke(null, 0, 0);
               ccUIEventListener.Get(rewardImg[0]).onClickV2.Invoke(null, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ccUIEventListener.Get(f_GetObject("BackBtn")).onClickV2.Invoke(null, 0, 0);
            }*/
            //鍵盤功能

            //imgListCtrl.On_Update();
        }
        protected override void On_UpdateGUI()
        {
            //throw new NotImplementedException();

        }

        protected override void On_Close()
        {
            //imgListCtrl.CenteringTargetBox(0, true);
            //imgListCtrl.On_Update();
        }

        protected override void On_Destory()
        {

        }

        void ImgUnlock(bool Unlock,int Imgindex)    //縮圖未解鎖則更改顏色
        {
            //if (Unlock)
            //{
            //    imgListCtrl.transform.GetChild(Imgindex).transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //}
            //else
            //{
            //    imgListCtrl.transform.GetChild(Imgindex).transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 0, 0, 222);
            //}
            //Transform boxTrans = listBoxs[Imgindex].transform;
            //boxTrans.GetChild(2).gameObject.SetActive(!Unlock);
            
            // Locked/Unlocked Effect.
            //boxTrans.GetChild(1).GetComponent<Image>().material = Unlock ? null : lockedMaterialRef;
        }

        void OpenMainMenu()
        {
            ccUIManage.GetInstance().f_SendMsg("UIP_Gallery", BaseUIMessageDef.UI_CLOSE);
            ccUIManage.GetInstance().f_SendMsg("UIP_MainMenu", BaseUIMessageDef.UI_OPEN);
        }
        private void On_ReturnBtnClick(GameObject go, object obj1, object obj2)
        {
            Camera.main.orthographic = false;
            OpenMainMenu();          
        }

        /// <summary>
        /// 當ImgList切換圖片時，大圖會隨著ListBox_Center變更而重新讀取
        /// 而該圖片未解鎖時，則顯示黑圖及解鎖條件
        /// </summary>
        void BigImgChange(bool value)
        {
            if (!value)
            {
                return;
            }

            //int nowImgIndex = imgListCtrl.GetCenteredContentID();
            //if (img_Unlock[nowImgIndex])
            //{
            //    tBigImg = AssetLoader.LoadAsset("img_scene.bundle", "scene" + (nowImgIndex + 1)) as Texture2D;
            //    //_ConditionText.SetActive(false);
            //}
            //else
            //{
            //    tBigImg = AssetLoader.LoadAsset("img_darkness.bundle", "darkness") as Texture2D;
            //    //_ConditionText.SetActive(true);
            //}
            //if (tBigImg != null)
            //{
            //    Sprite sprite = Sprite.Create(tBigImg, new Rect(0, 0, tBigImg.width, tBigImg.height), Vector2.zero);
            //    _Img_BigImg.sprite = sprite;
            //}

            //if(bigImgSprites[nowImgIndex] == null)
            //    bigImgSprites[nowImgIndex] = (Sprite)AssetLoader.LoadAsset("img_scene.bundle", "scene" + (nowImgIndex + 1));

            //_Img_BigImg.sprite = bigImgSprites[nowImgIndex];
        }

        void SwitchBigImg(int index)
        {
            if(img_Unlock[index]) // Unlocked Effect.
            {
                _Img_BigImg.transform.GetChild(1).localScale = Vector3.zero; // Locked image.
                _Img_BigImg.material = null;
            }
            else // Locked Effect.
            {
                _Img_BigImg.transform.GetChild(1).localScale = Vector3.one; // Locked image.
                _Img_BigImg.material = lockedMaterialRef;
            }

            _Img_BigImg.sprite = bigImgSprites[index];
        }

        /// <summary>
        /// 當該圖片已解鎖時點擊大圖，會將大圖放大至畫面大小
        /// 再次點擊則復原
        /// </summary>
        private void On_BigImgClick(GameObject go, object obj1, object obj2)
        {
            //if (!bigImgOpened && img_Unlock[imgListCtrl.GetCenteredContentID()])
            //{
            //    _BigImgRectTrans.sizeDelta = _CanvasSize;
            //    _BigImgRectTrans.localPosition = _CanvasPos;
            //    //Debug.Log("BBBBBBBigggggggggg");
            //    bigImgOpened = true;
            //}
            //else if (bigImgOpened)
            //{
            //    _BigImgRectTrans.sizeDelta = normalBigImgSize;
            //    _BigImgRectTrans.localPosition = normalBigImgPos;
            //    //Debug.Log("aaaaaAAAAAZZZZzzzz");
            //    bigImgOpened = false;
            //}
        }
    }
}