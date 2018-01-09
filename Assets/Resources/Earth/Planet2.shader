// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32880,y:32707,varname:node_3138,prsc:2|emission-6049-OUT,alpha-7421-A;n:type:ShaderForge.SFN_TexCoord,id:6356,x:31352,y:32786,varname:node_6356,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:4617,x:32370,y:32773,ptovrint:False,ptlb:node_4617,ptin:_node_4617,varname:node_4617,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd39419d5ee17e94eabc3b9dd6ac71f8,ntxv:0,isnm:False|UVIN-7495-UVOUT;n:type:ShaderForge.SFN_RemapRange,id:6184,x:31508,y:32786,varname:node_6184,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6356-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:4883,x:31664,y:32786,varname:node_4883,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6184-OUT;n:type:ShaderForge.SFN_Panner,id:7495,x:32190,y:32773,varname:node_7495,prsc:2,spu:1,spv:0|UVIN-1194-OUT,DIST-2089-OUT;n:type:ShaderForge.SFN_Tex2d,id:7421,x:32603,y:33158,ptovrint:False,ptlb:node_7421,ptin:_node_7421,varname:node_7421,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:328d77d95c33517419349e995503aad8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:7834,x:32155,y:32989,varname:node_7834,prsc:2,spu:1,spv:0|UVIN-1194-OUT,DIST-4770-OUT;n:type:ShaderForge.SFN_Slider,id:1265,x:31688,y:33213,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1265,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.06837607,max:1;n:type:ShaderForge.SFN_Time,id:1071,x:31845,y:33069,varname:node_1071,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2089,x:32012,y:33168,varname:node_2089,prsc:2|A-1071-T,B-1265-OUT;n:type:ShaderForge.SFN_Slider,id:1637,x:31688,y:33298,ptovrint:False,ptlb:CloudsSpeed,ptin:_CloudsSpeed,varname:node_1637,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1196581,max:1;n:type:ShaderForge.SFN_Multiply,id:4770,x:32061,y:33332,varname:node_4770,prsc:2|A-1071-T,B-1637-OUT;n:type:ShaderForge.SFN_Tex2d,id:7150,x:32334,y:32989,ptovrint:False,ptlb:Clouds,ptin:_Clouds,varname:node_7150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:412436e870e1eba46ad6026b8d242cdb,ntxv:0,isnm:False|UVIN-7834-UVOUT;n:type:ShaderForge.SFN_Add,id:8599,x:32539,y:32847,varname:node_8599,prsc:2|A-4617-RGB,B-1929-OUT,C-9946-OUT;n:type:ShaderForge.SFN_Slider,id:1096,x:31905,y:32403,ptovrint:False,ptlb:Rim,ptin:_Rim,varname:node_1096,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4.786325,max:10;n:type:ShaderForge.SFN_Length,id:808,x:31638,y:32529,varname:node_808,prsc:2|IN-6184-OUT;n:type:ShaderForge.SFN_Multiply,id:9946,x:32358,y:32584,varname:node_9946,prsc:2|A-6544-OUT,B-7929-RGB;n:type:ShaderForge.SFN_Color,id:7929,x:32113,y:32598,ptovrint:False,ptlb:AtmosphereColor,ptin:_AtmosphereColor,varname:node_7929,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1102941,c2:0.5582152,c3:1,c4:1;n:type:ShaderForge.SFN_Power,id:8903,x:32262,y:32442,varname:node_8903,prsc:2|VAL-808-OUT,EXP-1096-OUT;n:type:ShaderForge.SFN_Clamp01,id:6544,x:32468,y:32442,varname:node_6544,prsc:2|IN-8903-OUT;n:type:ShaderForge.SFN_Multiply,id:1929,x:32539,y:33006,varname:node_1929,prsc:2|A-7150-RGB,B-5563-OUT;n:type:ShaderForge.SFN_Slider,id:5563,x:32198,y:33225,ptovrint:False,ptlb:CloudsOpacity,ptin:_CloudsOpacity,varname:node_5563,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2051282,max:1;n:type:ShaderForge.SFN_Multiply,id:6049,x:32723,y:32817,varname:node_6049,prsc:2|A-6270-OUT,B-8599-OUT;n:type:ShaderForge.SFN_Length,id:774,x:31638,y:32635,varname:node_774,prsc:2|IN-4066-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:4066,x:31404,y:32550,varname:node_4066,prsc:2|IN-6356-UVOUT,IMIN-8488-OUT,IMAX-8732-OUT,OMIN-940-OUT,OMAX-6087-OUT;n:type:ShaderForge.SFN_Vector1,id:8488,x:31118,y:32536,varname:node_8488,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8732,x:31118,y:32590,varname:node_8732,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:940,x:30935,y:32698,ptovrint:False,ptlb:node_940,ptin:_node_940,varname:node_940,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:-1.068376,max:5;n:type:ShaderForge.SFN_Slider,id:6087,x:30971,y:32818,ptovrint:False,ptlb:node_6087,ptin:_node_6087,varname:node_6087,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0.4700857,max:5;n:type:ShaderForge.SFN_OneMinus,id:4612,x:31797,y:32635,varname:node_4612,prsc:2|IN-774-OUT;n:type:ShaderForge.SFN_Slider,id:8650,x:31623,y:32457,ptovrint:False,ptlb:node_8650,ptin:_node_8650,varname:node_8650,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6666667,max:3;n:type:ShaderForge.SFN_Power,id:6270,x:31951,y:32568,varname:node_6270,prsc:2|VAL-4612-OUT,EXP-8650-OUT;n:type:ShaderForge.SFN_Code,id:1194,x:30867,y:33017,varname:node_1194,prsc:2,code:ZgBsAG8AYQB0ACAAcQBzACAAPQAgAHAAbwB3ACgAeAAsADIAKQArAHAAbwB3ACgAeQAsADIAKQA7AAoAZgBsAG8AYQB0ACAAcwBxACAAPQAgAHMAcQByAHQAKABxAHMALQBzAHEAcgB0ACgAcQBzACoAKABxAHMALQA0ACoAcABvAHcAKAB4ACwAMgApACoAcABvAHcAKAB5ACwAMgApACkAKQApADsACgBmAGwAbwBhAHQAIABzAGcAIAA9ACAAcwBpAGcAbgAoAHgAKgB5ACkAOwAKAGYAbABvAGEAdAAgAHUAIAA9ACAAcwBxACoAcwBnAC8AKAB5ACoAcwBxAHIAdAAoADIAKQApADsACgBmAGwAbwBhAHQAIAB2ACAAPQAgAHMAcQAqAHMAZwAvACgAeAAqAHMAcQByAHQAKAAyACkAKQA7AAoAcgBlAHQAdQByAG4AIABmAGwAbwBhAHQAMgAoAHUALAAoAHYAKwAxACkALwAyACkAOwA=,output:1,fname:Function_node_1194,width:951,height:132,input:0,input:0,input_1_label:x,input_2_label:y|A-4883-R,B-4883-G;proporder:4617-7421-1265-1637-7150-1096-7929-5563-940-6087-8650;pass:END;sub:END;*/

Shader "Shader Forge/Planet_FG" {
    Properties {
        _node_4617 ("node_4617", 2D) = "white" {}
        _node_7421 ("node_7421", 2D) = "white" {}
        _Speed ("Speed", Range(0, 1)) = 0.06837607
        _CloudsSpeed ("CloudsSpeed", Range(0, 1)) = 0.1196581
        _Clouds ("Clouds", 2D) = "white" {}
        _Rim ("Rim", Range(0, 10)) = 4.786325
        _AtmosphereColor ("AtmosphereColor", Color) = (0.1102941,0.5582152,1,1)
        _CloudsOpacity ("CloudsOpacity", Range(0, 1)) = 0.2051282
        _node_940 ("node_940", Range(-5, 5)) = -1.068376
        _node_6087 ("node_6087", Range(-5, 5)) = 0.4700857
        _node_8650 ("node_8650", Range(0, 3)) = 0.6666667
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_4617; uniform float4 _node_4617_ST;
            uniform sampler2D _node_7421; uniform float4 _node_7421_ST;
            uniform float _Speed;
            uniform float _CloudsSpeed;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform float _Rim;
            uniform float4 _AtmosphereColor;
            uniform float _CloudsOpacity;
            uniform float _node_940;
            uniform float _node_6087;
            uniform float _node_8650;
            float2 Function_node_1194( float x , float y ){
            float qs = pow(x,2)+pow(y,2);
            float sq = sqrt(qs-sqrt(qs*(qs-4*pow(x,2)*pow(y,2))));
            float sg = sign(x*y);
            float u = sq*sg/(y*sqrt(2));
            float v = sq*sg/(x*sqrt(2));
            return float2(u,(v+1)/2);
            }
            
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_8488 = 0.0;
                float4 node_1071 = _Time + _TimeEditor;
                float2 node_6184 = (i.uv0*2.0+-1.0);
                float2 node_4883 = node_6184.rg;
                float2 node_1194 = Function_node_1194( node_4883.r , node_4883.g );
                float2 node_7495 = (node_1194+(node_1071.g*_Speed)*float2(1,0));
                float4 _node_4617_var = tex2D(_node_4617,TRANSFORM_TEX(node_7495, _node_4617));
                float2 node_7834 = (node_1194+(node_1071.g*_CloudsSpeed)*float2(1,0));
                float4 _Clouds_var = tex2D(_Clouds,TRANSFORM_TEX(node_7834, _Clouds));
                float3 emissive = (pow((1.0 - length((_node_940 + ( (i.uv0 - node_8488) * (_node_6087 - _node_940) ) / (1.0 - node_8488)))),_node_8650)*(_node_4617_var.rgb+(_Clouds_var.rgb*_CloudsOpacity)+(saturate(pow(length(node_6184),_Rim))*_AtmosphereColor.rgb)));
                float3 finalColor = emissive;
                float4 _node_7421_var = tex2D(_node_7421,TRANSFORM_TEX(i.uv0, _node_7421));
                return fixed4(finalColor,_node_7421_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
