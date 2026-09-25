#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec4 aBaseUV;
layout (location = 3) in vec4 aOverlayUV;
layout (location = 4) in vec3 aColor;

out vec2 texCoord;
out vec4 baseUV;
out vec4 overlayUV;
out vec3 color;

uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position =  vec4(aPos, 1.0) * view * projection;
    texCoord = aTexCoord;
    baseUV = aBaseUV;
    overlayUV = aOverlayUV;
    color = aColor;
}