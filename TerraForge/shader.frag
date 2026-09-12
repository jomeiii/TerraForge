#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform vec3 blockColor;
uniform bool useBlockColor;

void main()
{
    vec4 textureColor = texture(texture0, texCoord);

    if (useBlockColor)
    {
        outputColor = vec4(
        textureColor.rgb * blockColor,
        textureColor.a
        );
    }
    else
    {
        outputColor = textureColor;
    }
}