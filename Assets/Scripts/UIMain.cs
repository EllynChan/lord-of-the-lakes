using UnityEngine;

public class UIMain : MonoBehaviour
{
    private GameObject activePanel = null;

    public void OnClick(GameObject panelToShow)
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }
        panelToShow.SetActive(true);
        activePanel = panelToShow;
    }

    public void OnClickClose()
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }
        activePanel = null;
    }
}
