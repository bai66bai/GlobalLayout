using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenItem : MonoBehaviour, IPointerClickHandler
{
   public CtrScreenMove CtrScreenMove;
    private string name;
    private void Start()
    {
        name = transform.name;
    }
    public void OnPointerClick(PointerEventData eventData) => CtrScreenMove.OnClickScreen(name);
}
