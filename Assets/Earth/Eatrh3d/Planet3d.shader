// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33297,y:32586,varname:node_4013,prsc:2|emission-6804-OUT;n:type:ShaderForge.SFN_Tex2d,id:3389,x:32401,y:32095,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3389,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd39419d5ee17e94eabc3b9dd6ac71f8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6357,x:32372,y:32902,ptovrint:False,ptlb:Clounds,ptin:_Clounds,varname:node_6357,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:412436e870e1eba46ad6026b8d242cdb,ntxv:0,isnm:False|UVIN-8528-UVOUT;n:type:ShaderForge.SFN_Slider,id:7951,x:32334,y:33117,ptovrint:False,ptlb:CloudsOpaque,ptin:_CloudsOpaque,varname:node_7951,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4957265,max:1;n:type:ShaderForge.SFN_TexCoord,id:9256,x:32032,y:32902,varname:node_9256,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:8528,x:32195,y:32902,varname:node_8528,prsc:2,spu:1,spv:0|UVIN-9256-UVOUT,DIST-5629-OUT;n:type:ShaderForge.SFN_Slider,id:3482,x:31815,y:33179,ptovrint:False,ptlb:CloudsSpeed,ptin:_CloudsSpeed,varname:node_3482,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.001709402,max:0.1;n:type:ShaderForge.SFN_Multiply,id:5629,x:32136,y:33179,varname:node_5629,prsc:2|A-3482-OUT,B-1077-TTR;n:type:ShaderForge.SFN_Time,id:1077,x:31972,y:33241,varname:node_1077,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1088,x:32560,y:32902,varname:node_1088,prsc:2|A-6357-RGB,B-7951-OUT,C-9577-OUT;n:type:ShaderForge.SFN_Add,id:6804,x:32925,y:32633,varname:node_6804,prsc:2|A-3607-OUT,B-1088-OUT,C-7871-OUT;n:type:ShaderForge.SFN_Fresnel,id:3741,x:31872,y:32298,varname:node_3741,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7871,x:32344,y:32695,varname:node_7871,prsc:2|A-4744-OUT,B-1404-RGB,C-3103-OUT;n:type:ShaderForge.SFN_Color,id:1404,x:32114,y:32665,ptovrint:False,ptlb:Atmosphere,ptin:_Atmosphere,varname:node_1404,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5588235,c2:0.8174442,c3:1,c4:1;n:type:ShaderForge.SFN_Power,id:4744,x:32114,y:32507,varname:node_4744,prsc:2|VAL-3741-OUT,EXP-9082-OUT;n:type:ShaderForge.SFN_Slider,id:9082,x:31774,y:32504,ptovrint:False,ptlb:Rim,ptin:_Rim,varname:node_9082,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.564103,max:10;n:type:ShaderForge.SFN_Slider,id:3103,x:31774,y:32609,ptovrint:False,ptlb:RimForce,ptin:_RimForce,varname:node_3103,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5982906,max:10;n:type:ShaderForge.SFN_Tex2d,id:3769,x:32440,y:32427,ptovrint:False,ptlb:node_3769,ptin:_node_3769,varname:node_3769,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:806a1a080f7f26a4780c1dc57223e3e0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:3607,x:32784,y:32360,varname:node_3607,prsc:2|A-2360-OUT,B-9451-OUT,T-9577-OUT;n:type:ShaderForge.SFN_NormalVector,id:9474,x:31845,y:31887,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:6833,x:32221,y:32080,varname:node_6833,prsc:2,dt:1|A-618-OUT,B-9474-OUT;n:type:ShaderForge.SFN_Slider,id:8831,x:31999,y:32388,ptovrint:False,ptlb:ShadowPower,ptin:_ShadowPower,varname:node_8831,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.307692,max:10;n:type:ShaderForge.SFN_Power,id:3484,x:32221,y:32245,varname:node_3484,prsc:2|VAL-6833-OUT,EXP-8831-OUT;n:type:ShaderForge.SFN_Multiply,id:9451,x:32642,y:32193,varname:node_9451,prsc:2|A-3389-RGB,B-9577-OUT;n:type:ShaderForge.SFN_Slider,id:5677,x:31453,y:32128,ptovrint:False,ptlb:X,ptin:_X,varname:node_5677,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Append,id:618,x:31802,y:32138,varname:node_618,prsc:2|A-5677-OUT,B-8056-OUT,C-7768-OUT;n:type:ShaderForge.SFN_Slider,id:8056,x:31406,y:32214,ptovrint:False,ptlb:Y,ptin:_Y,varname:node_8056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.5811966,max:1;n:type:ShaderForge.SFN_Slider,id:7768,x:31431,y:32333,ptovrint:False,ptlb:Z,ptin:_Z,varname:node_7768,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.04273507,max:1;n:type:ShaderForge.SFN_Multiply,id:2360,x:32642,y:32427,varname:node_2360,prsc:2|A-3769-RGB,B-8959-OUT;n:type:ShaderForge.SFN_Slider,id:8959,x:32427,y:32614,ptovrint:False,ptlb:NightEarth,ptin:_NightEarth,varname:node_8959,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2307692,max:1;n:type:ShaderForge.SFN_Clamp01,id:9577,x:32447,y:32261,varname:node_9577,prsc:2|IN-3484-OUT;proporder:3389-6357-7951-3482-1404-9082-3103-3769-8831-5677-8056-7768-8959;pass:END;sub:END;*/

Shader "Shader Forge/Planet3d" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Clounds ("Clounds", 2D) = "white" {}
        _CloudsOpaque ("CloudsOpaque", Range(0, 1)) = 0.4957265
        _CloudsSpeed ("CloudsSpeed", Range(0, 0.1)) = 0.001709402
        _Atmosphere ("Atmosphere", Color) = (0.5588235,0.8174442,1,1)
        _Rim ("Rim", Range(0, 10)) = 2.564103
        _RimForce ("RimForce", Range(0, 10)) = 0.5982906
        _node_3769 ("node_3769", 2D) = "white" {}
        _ShadowPower ("ShadowPower", Range(0, 10)) = 2.307692
        _X ("X", Range(-1, 1)) = 1
        _Y ("Y", Range(-1, 1)) = 0.5811966
        _Z ("Z", Range(-1, 1)) = 0.04273507
        _NightEarth ("NightEarth", Range(0, 1)) = 0.2307692
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Clounds; uniform float4 _Clounds_ST;
            uniform float _CloudsOpaque;
            uniform float _CloudsSpeed;
            uniform float4 _Atmosphere;
            uniform float _Rim;
            uniform float _RimForce;
            uniform sampler2D _node_3769; uniform float4 _node_3769_ST;
            uniform float _ShadowPower;
            uniform float _X;
            uniform float _Y;
            uniform float _Z;
            uniform float _NightEarth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _node_3769_var = tex2D(_node_3769,TRANSFORM_TEX(i.uv0, _node_3769));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_9577 = saturate(pow(max(0,dot(float3(_X,_Y,_Z),i.normalDir)),_ShadowPower));
                float4 node_1077 = _Time + _TimeEditor;
                float2 node_8528 = (i.uv0+(_CloudsSpeed*node_1077.a)*float2(1,0));
                float4 _Clounds_var = tex2D(_Clounds,TRANSFORM_TEX(node_8528, _Clounds));
                float3 emissive = (lerp((_node_3769_var.rgb*_NightEarth),(_MainTex_var.rgb*node_9577),node_9577)+(_Clounds_var.rgb*_CloudsOpaque*node_9577)+(pow((1.0-max(0,dot(normalDirection, viewDirection))),_Rim)*_Atmosphere.rgb*_RimForce));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
