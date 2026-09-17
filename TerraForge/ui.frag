#version 330 core

in vec2 TexCoord;

out vec4 outputColor;

uniform sampler2D textTexture;

void main()
{
    float alpha = texture(textTexture, TexCoord).r;
    outputColor = vec4(1.0, 1.0, 1.0, alpha);
}