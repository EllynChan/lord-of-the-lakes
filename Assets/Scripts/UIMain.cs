using UnityEngine;

public class UIMain : MonoBehaviour
{
    [SerializeField] private GameObject[] relatedPanels;

    public void OnClick(GameObject panelToShow)
    {
        foreach (GameObject panel in relatedPanels)
        {
            panel.SetActive(false);
        }
        panelToShow.SetActive(true);
    }
}
