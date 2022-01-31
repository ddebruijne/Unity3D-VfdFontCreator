using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class TubeGen : MonoBehaviour
{
    public Color enabled;
    public Color disabled;
    
    public TMP_InputField inputField;
    public List<Button> segments;
    
    StringBuilder curText;
    public List<int> state;

    // Start is called before the first frame update
    void Start()
    {
        state = new List<int>(segments.Count);

        for (int i = 0; i < segments.Count; i++) {
            state.Add(0);
            if(segments[i] != null) {
                int closureIndex = i;
                segments[closureIndex].onClick.AddListener(delegate{SegmentClicked(closureIndex);});
            }
        }
        
        RefreshText();
    }

    void RefreshText() {
        curText = new StringBuilder("");

        for (int i = 0; i < segments.Count; i++) {
            curText.Append(state[i]);
            if(segments[i] == null)
                continue;

            Image img = segments[i].GetComponent<Image>();
            if(state[i] == 1)
                img.color = enabled;
            else
                img.color = disabled;
        }

        inputField.text = curText.ToString();
    }

    public void SegmentClicked(int segmentIndex)
    {
        Image img = segments[segmentIndex].GetComponent<Image>();

        if (state[segmentIndex] == 0) { 
            state[segmentIndex] = 1;
            img.color = enabled;
        } else {
            state[segmentIndex] = 0;
            img.color = disabled;
        }

        curText[segmentIndex] = (char)('0' + state[segmentIndex]);
        inputField.text = curText.ToString();
    }
}
