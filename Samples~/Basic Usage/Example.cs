using UnityEngine;

namespace Fluty.Inspector
{
    public enum ExampleEnum
    {
        Example1,
        Example2
    }

    public class Example : MonoBehaviour
    {
        [Fluty_ReadOnly]
        [SerializeField] private int dummyInteger = 5;

        public bool showDummyData = false;

        [Fluty_ShowIf(nameof(showDummyData), false)]
        [SerializeField] private int dummyData = 100;

        public bool showDummyButton = false;

        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_FoldoutGroup("Button Example")]
        [Fluty_Button("Button")]
        public void TestFunction()
        {
            Debug.Log("DUMMY Integer : " + dummyInteger);
        }

        [Fluty_FoldoutGroup("Button Example/Basic")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Number", true)]
        public void TestFunctionParameterNumber(int dummyInteger, float dummyFloat)
        {
            Debug.Log("DUMMY Integer : " + dummyInteger 
            + " || Dummy Float : " + dummyFloat);
        }

        [Fluty_FoldoutGroup("Button Example/Basic")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param String", true)]
        public void TestFunctionParameterString(string dummyString)
        {
            Debug.Log("DUMMY String : " + dummyString);
        }

        [Fluty_FoldoutGroup("Button Example/Basic")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Bool", true)]
        public void TestFunctionParameterBool(bool dummyBool)
        {
            Debug.Log("DUMMY Bool : " + dummyBool);
        }

        [Fluty_FoldoutGroup("Button Example/Struct")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Vector3", true)]
        public void TestFunctionParameterVector3(Vector3 dummyVector3)
        {
            Debug.Log("DUMMY Vector3 : " + dummyVector3);
        }

        [Fluty_FoldoutGroup("Button Example/Struct")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Vector2", true)]
        public void TestFunctionParameterVector2(Vector3 dummyVector2)
        {
            Debug.Log("DUMMY Vector3 : " + dummyVector2);
        }

        [Fluty_FoldoutGroup("Button Example/Struct")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Color", true)]
        public void TestFunctionParameterColor(Color dummyColor)
        {
            Debug.Log("DUMMY Color : " + dummyColor);
        }

        [Fluty_FoldoutGroup("Button Example/Enum")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Enum", true)]
        public void TestFunctionParameterEnum(ExampleEnum dummyEnum)
        {
            Debug.Log("DUMMY Enum : " + dummyEnum);
        }

        [Fluty_FoldoutGroup("Button Example/Object")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param GameObject", true)]
        public void TestFunctionParameterGameObject(GameObject dummyGameObject)
        {
            Debug.Log("DUMMY GameObject : " + dummyGameObject);
        }

        [Fluty_FoldoutGroup("Button Example/Object")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Transform", true)]
        public void TestFunctionParameterTransform(Transform dummyTransform)
        {
            Debug.Log("DUMMY Transform : " + dummyTransform);
        }

        [Fluty_FoldoutGroup("Button Example/Object")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Material", true)]
        public void TestFunctionParameterMaterial(Material dummyMaterial)
        {
            Debug.Log("DUMMY Material : " + dummyMaterial);
        }

        [Fluty_FoldoutGroup("Button Example/Object")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param Texture", true)]
        public void TestFunctionParameterTexture(Texture dummyTexture)
        {
            Debug.Log("DUMMY Texture : " + dummyTexture);
        }

        [Fluty_FoldoutGroup("Button Example/Object")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param AudioClip", true)]
        public void TestFunctionParameterAudioClip(AudioClip dummyAudioClip)
        {
            Debug.Log("DUMMY AudioClip : " + dummyAudioClip);
        }

        [Fluty_FoldoutGroup("Button Example/Object")]
        [Fluty_ShowIf(nameof(showDummyButton))]
        [Fluty_Button("Button Param ScriptableObject", true)]
        public void TestFunctionParameterScriptableObject(ScriptableObject dummyScriptableObject)
        {
            Debug.Log("DUMMY ScriptableObject : " + dummyScriptableObject);
        }
    }
}
