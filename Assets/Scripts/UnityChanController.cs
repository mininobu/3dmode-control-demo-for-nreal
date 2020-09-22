using NRKernal;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnityChanController : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public UnityChanSceneDirector director;

    private GameObject camera;

    private ControllerHandEnum m_CurrentDebugHand = ControllerHandEnum.Right;
    private ControllerButton currentDownButton;
    private bool unityChanEnterState = false;


    // Start is called before the first frame update
    void Start()
    {
        this.camera = GameObject.Find("NRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラ（左手右手）情報の取得
        // 複数のコントローラを使用している場合、使用したコントローラを判断するための処理。
        // 今後のハンドジェスチャーを見越しての処理か？
        if (NRInput.GetAvailableControllersCount() < 2)
        {
            m_CurrentDebugHand = NRInput.DomainHand;
        }
        else
        {
            if (NRInput.GetButtonDown(ControllerHandEnum.Right, ControllerButton.TRIGGER))
            {
                m_CurrentDebugHand = ControllerHandEnum.Right;
            }
            else if (NRInput.GetButtonDown(ControllerHandEnum.Left, ControllerButton.TRIGGER))
            {
                m_CurrentDebugHand = ControllerHandEnum.Left;
            }
        }

        // Trigger以外のボタン動作をハンドリングするにはIPointerDownHandlerではなく、Updateでハンドリングする
        // Triggerだけで良ければIPointerDownHandler/IPointerUpHandlerで実装するほうが良い
        if (NRInput.GetButtonDown(this.m_CurrentDebugHand, ControllerButton.APP))
        {
            this.director.WriteLog("OnPointerDown_APP");
            this.currentDownButton = ControllerButton.APP;

            if (this.unityChanEnterState)
            {
                GetComponent<Animator>().SetTrigger("doJump");
            }
        }
        else
        if (NRInput.GetButtonDown(this.m_CurrentDebugHand, ControllerButton.TRIGGER))
        {
            this.director.WriteLog("OnPointerDown_TRIGGER");
            this.currentDownButton = ControllerButton.TRIGGER;
        }
        else
        if (NRInput.GetButtonDown(this.m_CurrentDebugHand, ControllerButton.GRIP))
        {
            this.director.WriteLog("OnPointerDown_GRID");
            this.currentDownButton = ControllerButton.GRIP;
        }
        else
        if (NRInput.GetButtonDown(this.m_CurrentDebugHand, ControllerButton.HOME))
        {
            this.director.WriteLog("OnPointerDown_HOME");
            this.currentDownButton = ControllerButton.HOME;
        }
        else
        if (NRInput.GetButtonDown(this.m_CurrentDebugHand, ControllerButton.TOUCHPAD_BUTTON))
        {
            this.director.WriteLog("OnPointerDown_TOUCHPAD_BUTTON");
            this.currentDownButton = ControllerButton.TOUCHPAD_BUTTON;
        }

        if (NRInput.GetButtonUp(this.m_CurrentDebugHand, ControllerButton.APP) ||
            NRInput.GetButtonUp(this.m_CurrentDebugHand, ControllerButton.TRIGGER) ||
            NRInput.GetButtonUp(this.m_CurrentDebugHand, ControllerButton.GRIP) ||
            NRInput.GetButtonUp(this.m_CurrentDebugHand, ControllerButton.HOME) ||
            NRInput.GetButtonUp(this.m_CurrentDebugHand, ControllerButton.TOUCHPAD_BUTTON))
        {
            this.director.WriteLog("OnPointerUp_all");
            this.currentDownButton = 0;
        }

    }

    // Turn around.
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.director.WriteLog("OnPointerEnter");
        this.unityChanEnterState = true;

        transform.LookAt(this.camera.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.director.WriteLog("OnPointerExit");
        this.unityChanEnterState = false;
    }


}
