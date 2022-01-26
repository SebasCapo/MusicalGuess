using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionDisplay : MonoBehaviour {

    public Option option;

    public Sprite mysteryBox;
    public TextMeshProUGUI textInstance;
    public Image imageInstance;
    
    public void Setup() {
        imageInstance.sprite = mysteryBox;
        imageInstance.color = GameEvents.Instance.mysteryBoxColor;
        textInstance.text = string.Empty;
    }

    public void Reveal(HelpMode mode) {
        if(option.instrument == Instrument.NONE)
            return;    
        switch(mode) {
            case HelpMode.END:
            return;
            case HelpMode.TEXT:
            GameEvents.Instance.animatedText.Animate(textInstance, option.instrumentName);
            return;
            case HelpMode.IMAGE:
            imageInstance.color = GameEvents.Instance.defaultDisplayColor;
            imageInstance.sprite = option.image;
            return;
        }
    }

}
