Shader "Custom/Stencil_Show_Player"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        ColorMask 0
        //Tags { "RenderType"="Opaque" }
        LOD 80
        Stencil {
            Ref 1
            Comp Always
            Pass Replace
        }
        Pass{
            Tags{"Lightmode" = "Vertex"}
}

    }
}
