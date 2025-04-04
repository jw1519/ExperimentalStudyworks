using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using UnityEngine.XR.Management;

public class CheckDevice : MonoBehaviour
{
    List<Canvas> canvasList = new List<Canvas>();
    public GameObject mobilePlay;
    Camera Camera;
    public GameObject vrPlay;
    private void Awake()
    {
        CheckForDevice();
    }
    public void AddCanvas(Canvas canvas)
    {
        canvasList.Add(canvas);
        Camera = Camera.main;
    }
    public void CheckForDevice()
    {
        var xrLoader = XRGeneralSettings.Instance.Manager.activeLoader;
        //checks if vr headset is connected
        if (xrLoader != null) 
        {
            foreach (Canvas canvas in canvasList)
            {
                canvas.renderMode = RenderMode.WorldSpace;
                canvas.worldCamera = Camera.main;
                Debug.Log("VR");
            }
        }
        //its mobile
        else
        {
            // disables the controllers
            FindAnyObjectByType<XROrigin>().gameObject.SetActive(false);
            foreach (Canvas canvas in canvasList)
            {

                mobilePlay.SetActive(true);
                Camera.transform.SetParent(mobilePlay.transform);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

                int child = canvas.transform.childCount;
                if (canvas.CompareTag("leftArmCanvas"))
                {
                    for (int i = 0; i < child; i++)
                    {
                        RectTransform rt = canvas.transform.GetChild(i).GetComponent<RectTransform>();
                        if (canvas.transform.GetChild(i).CompareTag("Button"))
                        {
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                            rt.localPosition = new Vector2(0,30);
                        }
                        else
                        {
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                            rt.localPosition = new Vector2(160, -10);
                        }
                    }
                }
                else if (canvas.CompareTag("rightArmCanvas"))
                {
                    for (int i = 0; i < child; i++)
                    {
                        RectTransform rt = canvas.transform.GetChild(i).GetComponent<RectTransform>();
                        if (canvas.transform.GetChild(i).CompareTag("Panel"))
                        {
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1);
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1);
                            rt.gameObject.SetActive(false);
                            if (rt.gameObject.GetComponent<GridLayoutGroup>())
                            {
                                GridLayoutGroup grid = rt.gameObject.GetComponent<GridLayoutGroup>();
                                grid.cellSize = new Vector2(300, 100);
                                grid.childAlignment = TextAnchor.MiddleCenter;
                            }
                        }
                        else
                        {
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                            rt.localPosition = new Vector2(0, -10);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < child; i++)
                    {
                        if (canvas.transform.GetChild(i).CompareTag("Panel"))
                        {
                            canvas.transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1);
                            canvas.transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1);
                            canvas.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        else
                        {
                            canvas.transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                            canvas.transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                        }

                    }
                }
                canvas.transform.SetParent(mobilePlay.transform);
                //vrPlay.SetActive(false);
                Debug.Log("other");
            }
        }
    }
}
