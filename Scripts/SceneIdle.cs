using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIdle : MonoBehaviour
{

    public float timeoutSeconds = 5; // �û��޲����ĳ�ʱʱ�䣨�룩
    public float breakDuration = 20f;

    private float lastInteractionTime; // ��¼������ʱ��
    private float lastBreakTime = 0;

   

    void Start()
    {
        lastInteractionTime = Time.time;
        SceneManager.LoadSceneAsync("IdleScene", LoadSceneMode.Additive);
        lastBreakTime = Time.time;
    }

    void Update()
    {
        // ����Ƿ��м������������ƶ�
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            lastInteractionTime = Time.time;
            if (Time.time - lastBreakTime > breakDuration)
            {
                lastBreakTime = lastInteractionTime;
            }
        }

        // ����Ƿ�ʱ
        if (Time.time - lastInteractionTime > timeoutSeconds)
        {
            // �����������������
            Scene targetScene = SceneManager.GetSceneByName("IdleScene");
            if (targetScene.isLoaded)
            {
                GameObject[] rootObjects = targetScene.GetRootGameObjects();
                foreach (GameObject obj in rootObjects)
                {
                    if (obj.name == "MainObj")
                        obj.SetActive(true);
                }
            }

            // ���ص�ǰ����������
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.isLoaded)
            {
                GameObject[] rootObjects = currentScene.GetRootGameObjects();
                foreach (GameObject obj in rootObjects)
                {
                    if (obj.name == "MainObj")
                        obj.SetActive(false);
                }
            }

        }
    }

    private void OnEnable() => lastBreakTime = Time.time;


}