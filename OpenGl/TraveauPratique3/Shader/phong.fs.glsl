#version 330 core

in ATTRIB_VS_OUT
{
    vec2 texCoords;
    vec3 normal;
    vec3 lightDir[3];
    vec3 spotDir[3];
    vec3 obsPos;
} attribIn;

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

uniform sampler2D diffuseSampler;
uniform sampler2D specularSampler;

out vec4 FragColor;

void main()
{
    // TODO
    vec3 O = normalize(attribIn.obsPos);
    vec3 N = normalize(attribIn.normal);
    
    vec3 ambient = mat.emission + lightModelAmbient * mat.ambient;

    vec3 diffuse = vec3(0.0);
    vec3 specular = vec3(0.0);

    // calcul de la la diffusion et speculaire
    for (int i = 0; i < 3; i++) {

        vec3 L = normalize(attribIn.lightDir[i]);

        // DIFFUSION
        float diff = max(dot(N, L), 0.0);
        vec3 lightDiffuse_i = lights[i].diffuse * diff;

        // SPECULATION
        float spec = 0.0;
        vec3 V = normalize(attribIn.obsPos);
        if (useBlinn) { // modele blinn
            vec3 B = normalize(L + V);
            spec = pow(max(dot(N, B), 0.0), mat.shininess);

        } else { // modele phong
            vec3 R = reflect(-L, N);
            float cosPhi = max(dot(R, V), 0.0);
            spec = pow(cosPhi, mat.shininess);
        }
        vec3 lightSpecular_i = mat.specular * lights[i].specular * spec;

        vec3 lightAmbient_i = lights[i].ambient * mat.ambient;

        if(useSpotlight) {
            float lightAlignment = dot(-attribIn.lightDir[i], lights[i].spotDirection);
            
            if (lightAlignment > cos(radians(spotOpeningAngle))) {
                float attenuation = pow(lightAlignment, spotExponent);
                lightDiffuse_i *= attenuation;
                lightSpecular_i *= attenuation;
                lightAmbient_i *= attenuation;
            } else { 
                lightAmbient_i = vec3(0.0);
                lightSpecular_i = vec3(0.0);
                lightDiffuse_i = vec3(0.0);
            }
        }

        diffuse += lightDiffuse_i;
        specular += lightSpecular_i;
        ambient += lightAmbient_i;

    }
    vec3 diffuseTex = texture(diffuseSampler, attribIn.texCoords).rgb;
    vec3 color = ambient + diffuse + specular;
    FragColor = vec4(color*diffuseTex, 1.0);
}



