#version 330 core

in vec2 TexCoord;

out vec4 outputColor;

uniform vec4 textColor;
uniform sampler2D textTexture;
uniform float solidColorAlpha;

uniform bool useTexture;
uniform bool useSolidColor;

void main()
{
    if (useSolidColor)
    {
        // simple color
        outputColor = vec4(textColor.xyz, solidColorAlpha);
    }
    else if (useTexture)
    {
        // tuxture
        outputColor = texture(textTexture, TexCoord);
    }
    else
    {
        // text
        float alpha = texture(textTexture, TexCoord).r;
        outputColor = vec4(textColor.rgb, alpha * textColor.a);
    }
}