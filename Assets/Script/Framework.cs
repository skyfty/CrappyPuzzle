using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Localization;
using System;

public class Framework : MonoBehaviour
{
    public static string LanguageConfigName = "LanguageManager.currentLanguage";

    //public static string OssApiUrl = "http://dev.oss.touchmagic.cn";        //������
    public static string OssApiUrl = "http://oss.touchmagic.cn";          //�߻���


    /// <summary>
    /// ��ȡ��Ϸ���������
    /// </summary>
    public static BaseComponent Base
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static ConfigComponent Config
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���ݽ�������
    /// </summary>
    public static DataNodeComponent DataNode
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���ݱ������
    /// </summary>
    public static DataTableComponent DataTable
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static DebuggerComponent Debugger
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static DownloadComponent Download
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡʵ�������
    /// </summary>
    public static EntityComponent Entity
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ�¼������
    /// </summary>
    public static EventComponent Event
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ�ļ�ϵͳ�����
    /// </summary>
    public static FileSystemComponent FileSystem
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����״̬�������
    /// </summary>
    public static FsmComponent Fsm
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���ػ������
    /// </summary>
    public static LocalizationComponent Localization
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static NetworkComponent Network
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ����������
    /// </summary>
    public static ObjectPoolComponent ObjectPool
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static ProcedureComponent Procedure
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ��Դ�����
    /// </summary>
    public static ResourceComponent Resource
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static SceneComponent Scene
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static SettingComponent Setting
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static SoundComponent Sound
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static UIComponent UI
    {
        get;
        private set;
    }

    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public static WebRequestComponent WebRequest
    {
        get;
        private set;
    }

    private static bool IsInit = false;

    public static void InitBuiltinComponents()
    {
        if (IsInit) return;
        IsInit = true;

        Base = UnityGameFramework.Runtime.GameEntry.GetComponent<BaseComponent>();
        Config = UnityGameFramework.Runtime.GameEntry.GetComponent<ConfigComponent>();
        DataNode = UnityGameFramework.Runtime.GameEntry.GetComponent<DataNodeComponent>();
        DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
        Debugger = UnityGameFramework.Runtime.GameEntry.GetComponent<DebuggerComponent>();
        Download = UnityGameFramework.Runtime.GameEntry.GetComponent<DownloadComponent>();
        Entity = UnityGameFramework.Runtime.GameEntry.GetComponent<EntityComponent>();
        Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
        FileSystem = UnityGameFramework.Runtime.GameEntry.GetComponent<FileSystemComponent>();
        Fsm = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();
        Localization = UnityGameFramework.Runtime.GameEntry.GetComponent<LocalizationComponent>();
        Network = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent>();
        ObjectPool = UnityGameFramework.Runtime.GameEntry.GetComponent<ObjectPoolComponent>();
        Procedure = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
        Resource = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();
        Scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
        Setting = UnityGameFramework.Runtime.GameEntry.GetComponent<SettingComponent>();
        Sound = UnityGameFramework.Runtime.GameEntry.GetComponent<SoundComponent>();
        UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
        WebRequest = UnityGameFramework.Runtime.GameEntry.GetComponent<WebRequestComponent>();

        InitLanguageSettings();
        InitCurrentVariant();
    }


    public static void InitLanguageSettings()
    {
        if (Framework.Base.EditorResourceMode && Framework.Base.EditorLanguage != Language.Unspecified)
        {
            // �༭����Դģʽֱ��ʹ�� Inspector �����õ�����
            return;
        }

        Language language = Framework.Localization.Language;
        if (Framework.Setting.HasSetting(LanguageConfigName))
        {
            string languageString = Framework.Setting.GetString(LanguageConfigName);
            language = (Language)Enum.Parse(typeof(Language), languageString);
        }

        if (language != Language.English && language != Language.ChineseSimplified && language != Language.ChineseTraditional && language != Language.Korean)
        {
            // �����ݲ�֧�ֵ����ԣ���ʹ��Ӣ��
            language = Language.English;
        }
        Framework.Localization.Language = language;
        Framework.Localization.ReadData(AssetUtility.GetDictionaryAsset("Default", false), null);

    }

    public static void InitCurrentVariant()
    {
        if (Framework.Base.EditorResourceMode)
        {
            // �༭����Դģʽ��ʹ�� AssetBundle��Ҳ��û�б�����
            return;
        }

        string currentVariant = null;
        switch (Framework.Localization.Language)
        {
            case Language.English:
                currentVariant = "en-us";
                break;

            case Language.ChineseSimplified:
                currentVariant = "zh-cn";
                break;

            case Language.ChineseTraditional:
                currentVariant = "zh-tw";
                break;

            case Language.Korean:
                currentVariant = "ko-kr";
                break;

            default:
                currentVariant = "zh-cn";
                break;
        }

        Framework.Resource.SetCurrentVariant(currentVariant);
        Log.Info("Init current variant complete.");
    }

}
