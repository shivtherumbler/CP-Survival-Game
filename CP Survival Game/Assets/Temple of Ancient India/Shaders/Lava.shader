// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Bhasker Dee/Lava"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0.5
		_FlowSpeed("Flow Speed", Float) = 1
		_EmissionIntensity("Emission Intensity", Range( 0 , 5)) = 1.5
		_Emission("Emission", 2D) = "white" {}
		_FlowMap("Flow Map", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Direction("Direction", Vector) = (-0.5,1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest LEqual
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _FlowMap;
		uniform float4 _FlowMap_ST;
		uniform float2 _Direction;
		uniform float _FlowSpeed;
		uniform sampler2D _Emission;
		uniform float _EmissionIntensity;
		uniform float _Metallic;
		uniform float _Roughness;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_TexCoord281 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 uv_FlowMap = i.uv_texcoord * _FlowMap_ST.xy + _FlowMap_ST.zw;
			float2 temp_output_274_0 = ( (tex2D( _FlowMap, uv_FlowMap )).rg * ( _Direction * _Direction ) );
			float temp_output_269_0 = ( _Time.x * _FlowSpeed );
			float temp_output_275_0 = frac( temp_output_269_0 );
			float2 Add1298 = ( uv_TexCoord281 + ( temp_output_274_0 * temp_output_275_0 ) );
			float2 Add2299 = ( uv_TexCoord281 + ( temp_output_274_0 * frac( ( temp_output_269_0 + 0.5 ) ) ) );
			float3 lerpResult283 = lerp( UnpackNormal( tex2D( _Normal, Add1298 ) ) , UnpackNormal( tex2D( _Normal, Add2299 ) ) , 0);
			float3 Normal307 = lerpResult283;
			o.Normal = Normal307;
			float4 tex2DNode279 = tex2D( _Emission, Add1298 );
			float4 tex2DNode280 = tex2D( _Emission, Add2299 );
			float AbsLerp295 = abs( ( ( 0.5 - temp_output_275_0 ) / 0.5 ) );
			float4 lerpResult282 = lerp( tex2DNode279 , tex2DNode280 , AbsLerp295);
			float4 Emission306 = ( lerpResult282 * _EmissionIntensity );
			o.Emission = Emission306.rgb;
			float Metallic317 = _Metallic;
			float3 temp_cast_2 = (Metallic317).xxx;
			o.Specular = temp_cast_2;
			float lerpResult319 = lerp( tex2DNode279.a , tex2DNode280.a , AbsLerp295);
			float roughness320 = ( lerpResult319 * _Roughness );
			o.Smoothness = roughness320;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	
}
/*ASEBEGIN
Version=14003
907;99;655;456;-312.1352;2103.489;1.832213;True;False
Node;AmplifyShaderEditor.RangedFloatNode;264;-2094.484,-400.6456;Float;False;Property;_FlowSpeed;Flow Speed;2;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;265;-2205.833,-594.341;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;268;-2012.566,-1137.74;Float;True;Property;_FlowMap;Flow Map;5;0;Assets/flowmap.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;332;-1886.605,-941.7032;Float;False;Property;_Direction;Direction;7;0;-0.5,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;267;-1866.489,-392.999;Float;False;Constant;_Float4;Float 4;2;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;269;-1888.82,-540.562;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;272;-1610.349,-445.183;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;330;-1642.523,-982.4526;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;271;-1692.843,-1108.702;Float;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FractNode;275;-1621.206,-564.7137;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;284;-1221.309,-578.0531;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;274;-1440.954,-1060.662;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;276;-1168.269,-1071.917;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;281;-983.2563,-874.3502;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;287;-1284.456,-320.8779;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;304;-1382.397,-73.43449;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;285;-1012.768,-660.0927;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;278;-693.7196,-1013.686;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;290;-1042.535,-100.9513;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;288;-696.4764,-780.3171;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;298;-524.1366,-1011.554;Float;False;Add1;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;301;131.4546,-1570.39;Float;False;299;0;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;293;-856.692,-102.2379;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;299;-524.7429,-836.6702;Float;False;Add2;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;277;-64.33292,-1785.826;Float;True;Property;_Emission;Emission;4;0;Assets/flowingLava_v35_engine_4_5_Emissive_0000.tga;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;300;118.3167,-1894.312;Float;False;298;0;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;289;-34.87302,-1189.744;Float;True;Property;_Normal;Normal;6;0;Assets/flowingLava_v35_engine_4_5_Normal_0000.tga;True;bump;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;279;407.6649,-1853.171;Float;True;Property;_TextureSample5;Texture Sample 5;5;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;315;789.0609,-1428.406;Float;False;295;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;302;180.313,-911.2631;Float;False;299;0;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;280;385.4578,-1603.825;Float;True;Property;_TextureSample6;Texture Sample 6;6;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;295;-671.798,-111.4286;Float;False;AbsLerp;-1;True;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;829.8736,-1928.472;Float;False;295;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;303;190.5247,-1325.364;Float;False;298;0;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;319;1088.199,-1593.767;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;291;431.2438,-1290.738;Float;True;Property;_TextureSample7;Texture Sample 7;5;0;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;325;1012.958,-1766.633;Float;False;Property;_EmissionIntensity;Emission Intensity;3;0;1.5;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;292;403.5062,-1052.382;Float;True;Property;_TextureSample8;Texture Sample 8;5;0;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;323;1037.203,-1436.287;Float;False;Property;_Roughness;Roughness;1;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;297;825.4511,-953.71;Float;False;-1;0;1;OBJECT;0
Node;AmplifyShaderEditor.LerpOp;282;1076.338,-1881.415;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;322;1291.067,-1595.781;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;326;762.5496,-508.319;Float;False;Property;_Metallic;Metallic;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;283;1082.394,-1140.452;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;324;1307.272,-1866.317;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;320;1464.787,-1604.078;Float;False;roughness;-1;True;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;306;1494.856,-1870.029;Float;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;317;1041.019,-508.1095;Float;False;Metallic;-1;True;1;0;FLOAT;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;318;1495.305,-574.8724;Float;False;317;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;321;1491.4,-487.8398;Float;False;320;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;305;1497.133,-647.6726;Float;False;306;0;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;308;1509.242,-724.0708;Float;False;307;0;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;307;1306.241,-1138.689;Float;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1838.601,-748.1998;Float;False;True;2;Float;ASEMaterialInspector;0;0;StandardSpecular;Bhasker Dee/Lava;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;3;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0.0,0,0;False;7;FLOAT3;0.0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0.0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;269;0;265;1
WireConnection;269;1;264;0
WireConnection;272;0;269;0
WireConnection;272;1;267;0
WireConnection;330;0;332;0
WireConnection;330;1;332;0
WireConnection;271;0;268;0
WireConnection;275;0;269;0
WireConnection;284;0;272;0
WireConnection;274;0;271;0
WireConnection;274;1;330;0
WireConnection;276;0;274;0
WireConnection;276;1;275;0
WireConnection;287;0;267;0
WireConnection;287;1;275;0
WireConnection;304;0;267;0
WireConnection;285;0;274;0
WireConnection;285;1;284;0
WireConnection;278;0;281;0
WireConnection;278;1;276;0
WireConnection;290;0;287;0
WireConnection;290;1;304;0
WireConnection;288;0;281;0
WireConnection;288;1;285;0
WireConnection;298;0;278;0
WireConnection;293;0;290;0
WireConnection;299;0;288;0
WireConnection;279;0;277;0
WireConnection;279;1;300;0
WireConnection;280;0;277;0
WireConnection;280;1;301;0
WireConnection;295;0;293;0
WireConnection;319;0;279;4
WireConnection;319;1;280;4
WireConnection;319;2;315;0
WireConnection;291;0;289;0
WireConnection;291;1;303;0
WireConnection;292;0;289;0
WireConnection;292;1;302;0
WireConnection;282;0;279;0
WireConnection;282;1;280;0
WireConnection;282;2;296;0
WireConnection;322;0;319;0
WireConnection;322;1;323;0
WireConnection;283;0;291;0
WireConnection;283;1;292;0
WireConnection;283;2;297;0
WireConnection;324;0;282;0
WireConnection;324;1;325;0
WireConnection;320;0;322;0
WireConnection;306;0;324;0
WireConnection;317;0;326;0
WireConnection;307;0;283;0
WireConnection;0;1;308;0
WireConnection;0;2;305;0
WireConnection;0;3;318;0
WireConnection;0;4;321;0
ASEEND*/
//CHKSM=13B8456CD27A9BCCE3CE67F6867FC1390B852DE5