#version 400 core

// inputs
layout (triangles) in;
layout (triangle_strip, max_vertices = 3) out;

in DATA
{
    vec3 vFragPos;
    vec4 vColor;
    vec3 vNormal;
    vec2 vUV;

    mat4 vProjection;

    vec3 vLightPos;
    vec3 vViewPos;
} data_in[];

// outputs
out vec3 gFragPos;
out vec4 gColor;
out vec3 gNormal;
out vec2 gUV;

out vec3 gLightPos;
out vec3 gViewPos;

void main()
{
    // vec3 vector0 = vec3(gl_in[0].gl_Position - gl_in[1].gl_Position);
    // vec3 vector1 = vec3(gl_in[2].gl_Position - gl_in[1].gl_Position);
    // vec3 surfaceNormal = normalize(cross(vector0, vector1));


    gl_Position = data_in[0].vProjection * (gl_in[0].gl_Position);
    
    gFragPos = data_in[0].vFragPos;
    gColor = data_in[0].vColor;
    gNormal = data_in[0].vNormal;
    gUV = data_in[0].vUV;

    gLightPos = data_in[0].vLightPos;
    gViewPos = data_in[0].vViewPos;

    EmitVertex();

    gl_Position = data_in[1].vProjection * (gl_in[1].gl_Position);
    
    gFragPos = data_in[1].vFragPos;
    gColor = data_in[1].vColor;
    gNormal = data_in[1].vNormal;
    gUV = data_in[1].vUV;

    gLightPos = data_in[1].vLightPos;
    gViewPos = data_in[1].vViewPos;

    EmitVertex();

    gl_Position = data_in[2].vProjection * (gl_in[2].gl_Position);
    
    gFragPos = data_in[2].vFragPos;
    gColor = data_in[2].vColor;
    gNormal = data_in[2].vNormal;
    gUV = data_in[2].vUV;

    gLightPos = data_in[2].vLightPos;
    gViewPos = data_in[2].vViewPos;

    EmitVertex();

    EndPrimitive();
}