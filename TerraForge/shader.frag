#version 330

out vec4 outputColor;

in vec2 texCoord;
in vec4 baseUV;
in vec4 overlayUV;
in vec3 color;

uniform sampler2D texture0;

void main()
{
    vec2 atlasUV = mix(baseUV.xy, baseUV.zw, texCoord);
    vec4 textureColor = texture(texture0, atlasUV);

    if (overlayUV != vec4(0.0))
    {
        vec2 overlayAtlasUV = mix(overlayUV.xy, overlayUV.zw, texCoord);
        vec4 overlayColor = texture(texture0, overlayAtlasUV);

        textureColor = mix(textureColor, vec4(overlayColor.rgb * color, overlayColor.a), overlayColor.a);
    }
    else
    {
        textureColor = vec4(textureColor.rgb * color, textureColor.a);
    }

    outputColor = vec4(textureColor.rgb, textureColor.a);
}