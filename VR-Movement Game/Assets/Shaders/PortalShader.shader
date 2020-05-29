Shader "Custom/PortalShader"
{
    Properties{
      _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
          Tags { "RenderType" = "Opaque" }

          CGPROGRAM
          #pragma surface surf NoLighting
          //#pragma surface surf Lambert
          struct Input {
              float4 screenPos;
          };

          sampler2D _MainTex;

          void surf(Input IN, inout SurfaceOutput o) {
              float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
              o.Albedo = tex2D(_MainTex, screenUV).rgb * 0.5f;
          }

          // turns mat into unlit texture
          fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
          {
              fixed4 c;
              c.rgb = s.Albedo;
              c.a = s.Alpha;
              return c;
          }
          ENDCG
    }
}
