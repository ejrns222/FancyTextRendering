using TMPro;
using UnityEngine;

namespace LogicUI.FancyTextRendering
{
    [RequireComponent(typeof(TMP_Text))]
    public class MarkdownRenderer : MonoBehaviour
    {
        public enum SourceMode
        {
            DirectInput,
            TextAsset
        }

        [SerializeField]
        SourceMode _SourceMode = SourceMode.DirectInput;

        [SerializeField]
        [TextArea(minLines: 10, maxLines: 50)]
        string _Source;

        [SerializeField]
        TextAsset _SourceAsset;

        public SourceMode InputMode
        {
            get => _SourceMode;
            set
            {
                _SourceMode = value;
                RenderText();
            }
        }

        public string Source
        {
            get => _Source;
            set
            {
                _Source = value ?? string.Empty;
                _SourceMode = SourceMode.DirectInput;
                RenderText();
            }
        }

        public TextAsset SourceAsset
        {
            get => _SourceAsset;
            set
            {
                _SourceAsset = value;
                _SourceMode = SourceMode.TextAsset;
                RenderText();
            }
        }

        TMP_Text _TextMesh;
        public TMP_Text TextMesh
        {
            get
            {
                if (_TextMesh == null)
                    _TextMesh = GetComponent<TMP_Text>();

                return _TextMesh;
            }
        }

        private void OnValidate()
        {
            RenderText();
        }

        private void Awake()
        {
            RenderText();
        }


        public MarkdownRenderingSettings RenderSettings = MarkdownRenderingSettings.Default;

        private void RenderText()
        {
            Markdown.RenderToTextMesh(GetResolvedSource(), TextMesh, RenderSettings);
        }

        private string GetResolvedSource()
        {
            if (_SourceMode == SourceMode.TextAsset)
            {
                return _SourceAsset != null ? _SourceAsset.text : string.Empty;
            }

            return _Source ?? string.Empty;
        }
    }
}
