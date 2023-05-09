#version 330 core


struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininess;
};

// input
in vec3 vFragPos;  
in vec4 vColor;
in vec3 vNormal;
in vec2 vUV;

in vec3 vLightPos;
in vec3 vViewPos;

uniform Material material;


// output
out vec4 FragColor;

uniform sampler2D uTexture;

void main()
{
	vec4 tColor = texture(uTexture, vUV);
	
	float ambientStrength = 0.5;
	vec3 lightColor = vec3(1.0, 1.0, 1.0);
	float specularStrength = 0.1;

	// ambient
	vec3 ambient = lightColor * material.ambient * ambientStrength;

	// diffuse
	vec3 lightDir = normalize(vLightPos - vFragPos);
	vec3 diffuse = max(dot(vNormal, lightDir), 0.0) * material.diffuse;
	vec3 diffuseLight = lightColor * diffuse;

	// specular
	vec3 viewDir = normalize(vViewPos - vFragPos);
	vec3 reflectDir = reflect(-lightDir, vNormal);  

	float specular = pow(max(dot(viewDir, reflectDir), 0.0), 32) * specularStrength;
	vec3 specularLight = lightColor * specular * material.specular;

	FragColor = tColor * vec4(ambient + diffuseLight + specularLight, 1.0) * vColor;
}