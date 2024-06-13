using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;

    public float transitionTime = 1f;

    public void LoadNewScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }
   
    public void GoBack()
    {
        LoadNewScene(LevelStore.LastSceneName);
    }

    IEnumerator LoadLevel(string sceneName)
    {
        // 播放动画
        animator.SetTrigger("StartTrigger");

        // 等待动画播放完成
        yield return new WaitForSeconds(transitionTime);

        LevelStore.LastSceneName = SceneManager.GetActiveScene().name;

        // 切换场景
        SceneManager.LoadScene(sceneName);
    }
}
