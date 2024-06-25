using UnityEngine;

public class CtrLoadScene : MonoBehaviour
{
    public GameObject tempObject;
    public GameObject tempObject1;

    private string LoadScene;

    public LevelLoader LevelLoader;

    private bool isLoad = true;
    // Update is called once per frame
    void Update()
    {
        if (tempObject == null && tempObject1 == null)
        {
            if (isLoad)
            {
                LevelLoader.LoadNewScene(LoadScene);
                isLoad = false;
            }
        }
    }
    public void StartDestroy(string name)
    {
        LoadScene = name;
        Destroy(tempObject);
        Destroy(tempObject1);
    }

}
