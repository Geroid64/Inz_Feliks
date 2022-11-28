Shader "Custom/Stencil_ToBeSeenThrough"
{
    Properties
    {

        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        //_Color() = "red"{}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 80
        Stencil {
            Ref 1
            Comp NotEqual
            Pass Replace
        }
        Pass{
            Tags{"Lightmode" = "Vertex"}
}

    }
        }