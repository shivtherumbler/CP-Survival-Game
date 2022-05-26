// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Bhasker Dee/Tree"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_windamplitude("wind amplitude", Float) = 0.04
		_Power("Power", Float) = 1.5
		_Direction("Direction", Vector) = (1,0.5,0.5,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_MetallicRRoughnessA("Metallic (R) Roughness (A)", 2D) = "white" {}
		_Roughness("Roughness ", Range( 0 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _MetallicRRoughnessA;
		uniform float4 _MetallicRRoughnessA_ST;
		uniform float _Roughness;
		uniform float4 _Direction;
		uniform float _Power;
		uniform float _windamplitude;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 normalizeResult28 = normalize( ( _Direction + float4( ase_vertexNormal , 0.0 ) + float4( ase_vertex3Pos , 0.0 ) ) );
			float temp_output_12_0 = ( ( v.color.r * UNITY_PI ) + ( _Time.y * _Power ) );
			v.vertex.xyz += ( normalizeResult28 * v.color.r * ( ( sin( temp_output_12_0 ) + ( sin( ( temp_output_12_0 * 0.5 ) ) * 0.02 ) ) - cos( ( temp_output_12_0 * 10.0 ) ) ) * _windamplitude ).xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode35 = tex2D( _TextureSample0, uv_TextureSample0 );
			o.Albedo = tex2DNode35.rgb;
			float2 uv_MetallicRRoughnessA = i.uv_texcoord * _MetallicRRoughnessA_ST.xy + _MetallicRRoughnessA_ST.zw;
			float4 tex2DNode77 = tex2D( _MetallicRRoughnessA, uv_MetallicRRoughnessA );
			o.Metallic = tex2DNode77.r;
			o.Smoothness = ( tex2DNode77.a * _Roughness );
			o.Alpha = 1;
			clip( tex2DNode35.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	
}
/*ASEBEGIN
Version=14003
0;535;1154;493;1316.946;130.4633;1.6;True;False
Node;AmplifyShaderEditor.TimeNode;8;-2510,588.2;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;5;-2504.6,272.4;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;6;-2498.1,497.9;Float;False;1;0;FLOAT;1.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-2492.6,767.9999;Float;False;Property;_Power;Power;2;0;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-2234,607.9999;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-2261.6,412.2999;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-2094.1,698.4999;Float;False;Constant;_Float1;Float 1;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-2027.501,523.1001;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1883.3,679.1002;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;26;-1942.699,828.1;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;27;-1429.3,867.0997;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1784.101,789.1;Float;False;Constant;_Float2;Float 2;0;0;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1522.9,925.4999;Float;False;Constant;_Float3;Float 3;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;17;-1708.4,677.3;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1550.9,761.1;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;42;-1753.399,362.9001;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;30;-1471.999,179.9004;Float;False;Property;_Direction;Direction;3;0;1,0.5,0.5,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1350.9,855.4999;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;43;-1569.399,429.8004;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;16;-1721.8,554.7;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;24;-1181.3,868.7001;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-1104.799,302.2005;Float;False;3;3;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0,0;False;2;FLOAT3;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1375.1,580.7;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;23;-988.7,585.2999;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-986.299,695.0005;Float;False;Property;_windamplitude;wind amplitude;1;0;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;38;-1759.299,489.2002;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;28;-966.9995,378.3004;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-715.1993,417.9001;Float;False;4;4;0;FLOAT4;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;2;FLOAT;0,0,0,0;False;3;FLOAT;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;77;-675.1692,68.11987;Float;True;Property;_MetallicRRoughnessA;Metallic (R) Roughness (A);6;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;74;-693.7874,292.7704;Float;False;Property;_Roughness;Roughness ;6;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;73;-289.2971,897.8;Float;False;Constant;_Color1;Color 1;10;0;0.5294118,0.4709939,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;76;-282.1072,427.4718;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-336.1062,308.7992;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;35;-539.8689,-141.7705;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-16.9,254.8;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Bhasker Dee/Tree;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Masked;0.5;True;True;0;True;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;0;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;8;2
WireConnection;10;1;11;0
WireConnection;9;0;5;1
WireConnection;9;1;6;0
WireConnection;12;0;9;0
WireConnection;12;1;10;0
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;26;0;12;0
WireConnection;27;0;26;0
WireConnection;17;0;13;0
WireConnection;18;0;17;0
WireConnection;18;1;21;0
WireConnection;20;0;27;0
WireConnection;20;1;22;0
WireConnection;16;0;12;0
WireConnection;24;0;20;0
WireConnection;29;0;30;0
WireConnection;29;1;42;0
WireConnection;29;2;43;0
WireConnection;19;0;16;0
WireConnection;19;1;18;0
WireConnection;23;0;19;0
WireConnection;23;1;24;0
WireConnection;38;0;5;1
WireConnection;28;0;29;0
WireConnection;25;0;28;0
WireConnection;25;1;38;0
WireConnection;25;2;23;0
WireConnection;25;3;34;0
WireConnection;76;0;25;0
WireConnection;78;0;77;4
WireConnection;78;1;74;0
WireConnection;0;0;35;0
WireConnection;0;3;77;1
WireConnection;0;4;78;0
WireConnection;0;10;35;4
WireConnection;0;11;76;0
ASEEND*/
//CHKSM=25382BD78AA0FADACE30CA38ADD90118D3D1F22E