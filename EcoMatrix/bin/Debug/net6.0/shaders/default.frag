#version 400 core


struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininess;
};

struct DirLight {
    vec3 direction;
  
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

struct PointLight {    
    vec3 position;
    
    float constant;
    float linear;
    float quadratic;  

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

// inputs
in vec3 gFragPos;  
in vec4 gColor;
in vec3 gNormal;
in vec2 gUV;

in vec3 gViewPos;

uniform Material material;
uniform DirLight dirLight;
uniform vec3 lightColor;

// #define NR_POINT_LIGHTS 4
// uniform PointLight pointLights[NR_POINT_LIGHTS];

float ambientStrength = 0.5;
float specularStrength = 0.1;

// outputs
out vec4 FragColor;

uniform sampler2D uTexture;

vec3 CalculateDirectionalLight(DirLight light, vec3 normal, vec3 viewDir)
{
    // ambient
	vec3 ambient = lightColor * material.ambient * ambientStrength;

	// diffuse
	vec3 diffuse = max(dot(normal, light.direction), 0.0) * material.diffuse;
	vec3 diffuseLight = lightColor * diffuse;

	// specular;
	vec3 reflectDir = reflect(-light.direction, normal);  

	float specular = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess) * specularStrength;

    vec3 specularLight = lightColor * specular * material.specular;

    return ambient + diffuseLight + specularLight;
}

vec3 CalculatePointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos);
    // diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    // specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    // attenuation
    float distance = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + 
  			     light.quadratic * (distance * distance));
    
    // combine results
    vec3 ambient  = light.ambient;
    vec3 diffuse  = light.diffuse  * diff;
    vec3 specular = light.specular * spec;

    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;

    return (ambient + diffuse + specular);
}

void main()
{
	vec4 tColor = texture(uTexture, gUV);

    vec3 norm = gNormal;
    vec3 viewDir = normalize(gViewPos - gFragPos);

    // phase 1: Directional lighting
    vec3 result = CalculateDirectionalLight(dirLight, norm, viewDir);

    // phase 2: Point lights
    // for(int i = 0; i < NR_POINT_LIGHTS; i++)
    // {
    //    result += CalculatePointLight(pointLights[i], norm, vFragPos, viewDir);
    // }

	FragColor = tColor * vec4(result, 1.0) * gColor;
}