#version 330 core

// input
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;
layout (location = 2) in vec3 aNormal;
layout (location = 3) in vec2 aUV;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

uniform vec3 uLightPos;
uniform vec3 uViewPos;

// output
out vec3 vFragPos;
out vec4 vColor;
out vec3 vNormal;
out vec2 vUV;

out vec3 vLightPos;
out vec3 vViewPos;

void main()
{
    vFragPos = vec3(uModel * vec4(aPosition, 1.0));
    vColor = aColor;
    vNormal = aNormal;
    vUV = aUV;

    vLightPos = uLightPos;
    vViewPos = uViewPos;

    gl_Position = uProjection * uView * uModel * vec4(aPosition, 1.0);
}