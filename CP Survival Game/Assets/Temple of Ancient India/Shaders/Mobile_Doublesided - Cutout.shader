// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Bhasker Dee/Mobile - Doublesided - Cutout"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Color("Color", Color) = (0.8,0.8,0.8,1)
		_Albedo("Albedo", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows 
		struct Input
		{
			fixed2 uv_texcoord;
		};

		uniform fixed4 _Color;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			fixed4 tex2DNode2 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = ( _Color * tex2DNode2 ).rgb;
			o.Alpha = 1;
			clip( tex2DNode2.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	
}
/*ASEBEGIN
Version=14003
341;307;1160;736;982.5113;468.5756;1.419083;True;True
Node;AmplifyShaderEditor.ColorNode;8;-486.4872,-338.1434;Float;False;Property;_Color;Color;1;0;0.8,0.8,0.8,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-562.2053,-137.523;Float;True;Property;_Albedo;Albedo;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-147.3,-156.8;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Fixed;False;True;2;Fixed;ASEMaterialInspector;0;0;Lambert;Bhasker Dee/Mobile - Doublesided - Cutout;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Masked;0.5;True;True;0;True;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;8;0
WireConnection;7;1;2;0
WireConnection;0;0;7;0
WireConnection;0;10;2;4
ASEEND*/
//CHKSM=FE3D1DD242FC96BE51F3E8DE1B727D5EFC08C275