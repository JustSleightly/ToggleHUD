Shader "Quantum/JustSleightly/ToggleHUD"{
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_UIColor ("UI Color", Color) = (1,1,1,1)
        _RotX ("X Position", Float) = 0.0
        _RotY ("Y Position", Float) = 0.0
        _Dist ("Distance", Float) = 1.0
        _Width ("Width", Float) = 6.0
        _Height ("Height", Float) = 6.0
        _Rows ("Rows", Int) = 4
        _Columns ("Columns", Int) = 4
        [Toggle(FLIP_HORIZONTAL)] _FlipHorizontal ("Flip Horizontal Order", Float) = 0.0
        [Toggle(FLIP_VERTICAL)] _FlipVertical ("Flip Vertical Order", Float) = 0.0
        [Toggle(ORDER_VERTICAL)] _OrderVertical ("Vertical Icon Order", Float) = 0.0
        _Toggle_0 ("Toggle 0", Int) = 1
        _Toggle_1 ("Toggle 1", Int) = 1
        _Toggle_2 ("Toggle 2", Int) = 1
        _Toggle_3 ("Toggle 3", Int) = 1
        _Toggle_4 ("Toggle 4", Int) = 1
        _Toggle_5 ("Toggle 5", Int) = 1
        _Toggle_6 ("Toggle 6", Int) = 1
        _Toggle_7 ("Toggle 7", Int) = 1
        _Toggle_8 ("Toggle 8", Int) = 1
        _Toggle_9 ("Toggle 9", Int) = 1
        _Toggle_10 ("Toggle 10", Int) = 1
        _Toggle_11 ("Toggle 11", Int) = 1
        _Toggle_12 ("Toggle 12", Int) = 1
        _Toggle_13 ("Toggle 13", Int) = 1
        _Toggle_14 ("Toggle 14", Int) = 1
        _Toggle_15 ("Toggle 15", Int) = 1
    }
    SubShader{
        Tags { "RenderType" = "Transparent" "QUEUE"="Overlay" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always
        ZWrite Off
        Cull Front

        Pass{
            CGPROGRAM
			#pragma shader_feature_local FLIP_HORIZONTAL
			#pragma shader_feature_local FLIP_VERTICAL
            #pragma shader_feature_local ORDER_VERTICAL
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _UIColor;
            float _RotX;
            float _RotY;
            float _Dist;
            float _Width;
            float _Height;
            uint _Rows;
            uint _Columns;

            int _Toggle_0,  _Toggle_1,  _Toggle_2,  _Toggle_3,
                _Toggle_4,  _Toggle_5,  _Toggle_6,  _Toggle_7,
                _Toggle_8,  _Toggle_9,  _Toggle_10, _Toggle_11,
                _Toggle_12, _Toggle_13, _Toggle_14, _Toggle_15;

            v2f vert (appdata v){
                v2f o;
                float2 aDir = float2(v.uv.xy*2.0-1.0);
                float angleX = (_RotX + aDir.x*_Width*_Columns/2.0)*UNITY_TWO_PI/360.0;
                float angleY = (_RotY - aDir.y*_Height*_Rows/2.0)*UNITY_TWO_PI/360.0;
                #ifdef USING_STEREO_MATRICES
                float4 otherCamWorldPos = float4(unity_StereoCameraToWorld[1-unity_StereoEyeIndex][0][3],
                                                 unity_StereoCameraToWorld[1-unity_StereoEyeIndex][1][3],
                                                 unity_StereoCameraToWorld[1-unity_StereoEyeIndex][2][3],
                                                 1.0);
                float3 otherCamViewPos = mul(UNITY_MATRIX_V, otherCamWorldPos).xyz;
                float3 pos = float3(0.0, 0.0, -_Dist*length(otherCamViewPos/0.063));
                #else
                float3 pos = float3(0.0, 0.0, -_Dist);
                #endif
                float3x3 rotX = float3x3(1, 0, 0,
                                         0, cos(angleY), sin(angleY),
                                         0, -sin(angleY), cos(angleY));
                float3x3 rotY = float3x3(cos(angleX), 0, -sin(angleX),
                                         0, 1, 0,
                                         sin(angleX), 0, cos(angleX));
                
                #ifdef USING_STEREO_MATRICES
                o.vertex = mul(UNITY_MATRIX_P, float4(mul(rotY, mul(rotX, pos)) + otherCamViewPos/2.0, 1.0));
                #else
                o.vertex = mul(UNITY_MATRIX_P, float4(mul(rotY, mul(rotX, pos)), 1.0));
                #endif

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target{
                uint composite = _Toggle_0      | _Toggle_1<<1   | _Toggle_2<<2   | _Toggle_3<<3
                              | _Toggle_4<<4   | _Toggle_5<<5   | _Toggle_6<<6   | _Toggle_7<<7
                              | _Toggle_8<<8   | _Toggle_9<<9   | _Toggle_10<<10 | _Toggle_11<<11
                              | _Toggle_12<<12 | _Toggle_13<<13 | _Toggle_14<<14 | _Toggle_15<<15;
                uint2 position = uint2(i.uv.x*_Columns, i.uv.y*_Rows);
                #ifdef FLIP_HORIZONTAL
                    position.x = _Columns - 1 - position.x;
                #endif
                #ifdef FLIP_VERTICAL
                    position.y = _Rows - 1 - position.y;
                #endif

                #ifdef ORDER_VERTICAL
                    uint index = position.x*_Rows + position.y;
                #else
                    uint index = position.x + position.y*_Columns;
                #endif
                
                float2 localSample = float2(frac(i.uv.x*_Columns), frac(i.uv.y*_Rows));
                float2 samplePos = (float2(index%4, index/4)+localSample)/4.0;

                if((composite & (1<<index)) == 0 || index >= 16) discard;
                fixed4 col = tex2Dlod(_MainTex, float4(samplePos, 0.0, 0.0));
                return col * _UIColor;
            }
            ENDCG
        }
    }
    CustomEditor "ToggleHUDEditor"
}
