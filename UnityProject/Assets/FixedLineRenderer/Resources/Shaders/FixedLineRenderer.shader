Shader "FixedLineRenderer" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" { }
	_Width ("Width", Range(0.001, 10)) = 1
  }
  CGINCLUDE
  #include "UnityCG.cginc"
  
  struct v2f {
    float4 pos : POSITION;
    float2 uv : TEXCOORD0;
    fixed4 color;
  };

  struct appdata {
    float4 vertex : POSITION;
    float4 normal : NORMAL;
    float2 texcoord : TEXCOORD0;
    float2 texcoord1 : TEXCOORD1;
    fixed4 color : COLOR;
  };

  sampler2D _MainTex;
  float _Width;

  v2f vert(appdata v)
  {
    v2f o;
    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    float4 diff = mul(UNITY_MATRIX_MVP, v.vertex + v.normal);

    float4 offset = (diff/diff.w - o.pos/o.pos.w);

    float2x2 rot = float2x2(0, -1, 1, 0);
    float2 rotatedOffset = mul(rot, normalize(offset.xy)) * (v.texcoord1 * -sign(diff.z)) * 0.009 * (o.pos.z);
    o.pos.xy += rotatedOffset * _Width;
    o.uv = v.texcoord;
    o.color = v.color;
    
    return o;
  }
  
  
  fixed4 frag(v2f i) : COLOR
  {
    fixed4 ret = tex2D(_MainTex, i.uv);
    ret.rgb *= i.color.rgb;
    ret.a *= i.color.a;
    return ret;
  }
  
  ENDCG

  
  SubShader {
    Tags { "RenderType"="Transparent"
		"Queue" = "Transparent" }
    
    
    Pass {
      Cull Back
      ZWrite Off
      Lighting Off
      Blend SrcAlpha OneMinusSrcAlpha
      Offset -1, -1

      CGPROGRAM
	  #pragma fragmentoption ARB_precision_hint_fastest
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
    }
    
  }
}
