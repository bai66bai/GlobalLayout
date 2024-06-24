using UnityEngine;

public class CtrLoadScene : MonoBehaviour
{
    public GameObject tempObject;
    public GameObject tempObject1;

    public string LoadScene;

    public LevelLoader LevelLoader;

    // Update is called once per frame
    void Update()
    {
        if (tempObject == null && tempObject1 == null)
        {
            Debug.Log("Ïú»Ù");
            LevelLoader.LoadNewScene(LoadScene);
        }
        else
        {
            
        }
    }
    public void StartDestroy()
    {
        Debug.Log("1111");
        Destroy(tempObject);
        Destroy(tempObject1);
    }

}
