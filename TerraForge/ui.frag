#version 330 core

in vec2 TexCoord;

out vec4 outputColor;

uniform vec4 textColor;
uniform sampler2D textTexture;
uniform bool useSolidColor;

void main()
{
    if (useSolidColor)
    {
        outputColor = textColor;
    }
    else
    {
        float alpha = texture(textTexture, TexCoord).r;
        outputColor = vec4(textColor.rgb, alpha * textColor.w);
    }
}