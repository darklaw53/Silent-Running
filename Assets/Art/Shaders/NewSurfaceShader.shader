Shader "Custom/NewSurfaceShader"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert alpha
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            void surf(Input IN, inout SurfaceOutputAlpha o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Alpha = c.a;
            }

            // Stencil setup
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }

            ENDCG
    }
        FallBack "Sprites/Default"
}