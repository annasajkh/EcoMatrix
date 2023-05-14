#version 400 core

// inputs
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;
layout (location = 2) in vec3 aNormal;
layout (location = 3) in vec2 aUV;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

uniform vec3 uLightPos;
uniform vec3 uViewPos;

// outputs
out DATA
{
    vec3 vFragPos;
    vec4 vColor;
    vec3 vNormal;
    vec2 vUV;

    mat4 vProjection;

    vec3 vLightPos;
    vec3 vViewPos;
} data_out;

void main()
{
    data_out.vFragPos = vec3(uModel * vec4(aPosition, 1.0));
    data_out.vColor = aColor;
    data_out.vNormal = aNormal;
    data_out.vUV = aUV;

    data_out.vProjection = uProjection;

    data_out.vLightPos = uLightPos;
    data_out.vViewPos = uViewPos;

    gl_Position = uView * uModel * vec4(aPosition, 1.0);
}