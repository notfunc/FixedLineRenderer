Shader "FixedLineRendererGuard" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" { }
  }
  CGINCLUDE
  #include "UnityCG.cginc"
  
  struct v2f {
    float4 pos : POSITION;
  };

  struct appdata {
    float4 vertex : POSITION;
    float4 normal : NORMAL;
  };

  sampler2D _MainTex;

  
  v2f vert(appdata v)
  {
    v2f o;
    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    float4 norm = mul(UNITY_MATRIX_IT_MV, v.normal);
    return o;
  }

  
  fixed4 frag(v2f i) : COLOR
  {
    return fixed4(0.0,0.0,0,0);
  }
  
  ENDCG

  
  SubShader {
    Tags { "Queue" = "Geometry" }
    
    
    Pass {
      Cull Back
      ZWrite On
      Lighting Off
      Blend Zero One
      Offset 1, 1

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
    }
    
  }
}
