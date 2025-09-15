#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec2 texCoords;
layout (location = 2) in vec3 normal;

out ATTRIB_VS_OUT
{
    vec2 texCoords;
    vec3 emission;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
} attribOut;

uniform mat4 mvp;
uniform mat4 view;
uniform mat4 modelView;
uniform mat3 normalMatrix;

struct Material
{
    vec3 emission;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

struct UniversalLight
{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    vec3 position;
    vec3 spotDirection;
};

layout (std140) uniform LightingBlock
{
    Material mat;
    UniversalLight lights[3];
    vec3 lightModelAmbient;
    bool useBlinn;
    bool useSpotlight;
    bool useDirect3D;
    float spotExponent;
    float spotOpeningAngle;
};

void main()
{
    // TODO

    vec3 pos = vec3(modelView * vec4(position, 1.0));
    vec3 N = normalize(normalMatrix * normal);
    
    vec3 ambient = mat.emission + lightModelAmbient * mat.ambient;
    vec3 totalDiffuse = vec3(0.0);
    vec3 totalSpecular = vec3(0.0);
    
    // calcul de la la diffusion et speculaire
    for (int i = 0; i < 3; i++) {
        vec3 lightPos = vec3(view * vec4(lights[i].position, 1.0));
        vec3 L = normalize(lightPos - pos);
        float distance = length(lightPos - pos);
        
        // DIFFUSION
        float diff = max(dot(N, L), 0.0);
        vec3 diffuse = diff * lights[i].diffuse;
        
        // SPECULATION
        vec3 V = normalize(-pos);
        float spec = 0.0;
        if (useBlinn) { // modele blinn
            vec3 H = normalize(L + V);
            spec = pow(max(dot(N, H), 0.0), mat.shininess);

        } else { // modele phong
            vec3 R = reflect(-L, N);
            spec = pow(max(dot(V, R), 0.0), mat.shininess);
        }
        vec3 specular = spec * lights[i].specular * mat.specular;

        vec3 lightAmbient_i = lights[i].ambient * mat.ambient;
        
        if (useSpotlight) {
            vec3 spotDir = normalize(mat3(view) * lights[i].spotDirection);
            float theta = dot(-L, spotDir);
            float epsilon = cos(radians(spotOpeningAngle));  

            if (theta > epsilon) {
                float spotAttenuation = pow(theta, spotExponent);
                diffuse *= spotAttenuation;
                specular *= spotAttenuation;
                lightAmbient_i *= spotAttenuation;
            } else {
                diffuse = vec3(0.0);
                specular = vec3(0.0);
                lightAmbient_i = vec3(0.0);
            }
        }

        totalDiffuse += diffuse;
        totalSpecular += specular;
        ambient += lightAmbient_i;
    }

    attribOut.texCoords = texCoords;
    attribOut.emission = mat.emission;
    attribOut.ambient = ambient;
    attribOut.diffuse = totalDiffuse;
    attribOut.specular = totalSpecular;

    gl_Position = mvp * vec4(position, 1.0);
    attribOut.texCoords = texCoords;
}
