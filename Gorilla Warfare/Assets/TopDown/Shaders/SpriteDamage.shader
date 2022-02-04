Shader "Sprites/SpriteDamange"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _DamageColor ("Damage Color", Color) = (1,1,1,1)
        _DamageEffect ("Damage Range", Range(0,1)) = 0
        [MaterialToggle] _UseRainbow ("Use Rainbow", Float) = 0
        _RainbowTex ("Rainbow Tex", 2D) = "white" {}
        _Opacity ("Rainbow Tex", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
            };
            
            fixed4 _Color;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                    OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
            sampler2D _RainbowTex;
            sampler2D _AlphaTex;
            float _AlphaSplitEnabled;
            fixed4 _DamageColor;
            float _DamageEffect;
            float _UseRainbow;
            float _Opacity;

            fixed4 SampleSpriteTexture (float2 uv)
            {
                fixed4 color = tex2D (_MainTex, uv);

                #if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                    if (_AlphaSplitEnabled)
                    color.a = tex2D (_AlphaTex, uv).r;
                #endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

                return color;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                if (_UseRainbow != 1)
                {
                    c.rgb *= c.a;
                    return lerp(c,_DamageColor*c.a,_DamageEffect);
                }
                else
                {
                    float raninbowUV = IN.texcoord + 90 * _Time.x;
                    fixed4 colRainbow = tex2D (_RainbowTex, raninbowUV);
                    return colRainbow * c.a * _Opacity;
                }
            }
            ENDCG
        }
    }
}