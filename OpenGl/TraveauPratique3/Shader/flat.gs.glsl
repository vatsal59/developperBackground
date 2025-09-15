#version 330 core

layout(triangles) in;
layout(triangle_strip, max_vertices = 3) out;

in ATTRIB_OUT
{
    vec3 position;
    vec2 texCoords;
} attribIn[];

out ATTRIB_VS_OUT
{
    vec2 texCoords;    
    vec3 emission;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
} attribOut;

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
    vec3 side1 = vec3(modelView * vec4(attribIn[1].position, 1.0)) - vec3(modelView * vec4(attribIn[0].position, 1.0));
    vec3 side2 = vec3(modelView * vec4(attribIn[2].position, 1.0)) - vec3(modelView * vec4(attribIn[0].position, 1.0));
    vec3 N = normalize(cross(side1, side2));

    vec3 pos0 = vec3(modelView * vec4(attribIn[0].position, 1.0));
    vec3 pos1 = vec3(modelView * vec4(attribIn[1].position, 1.0));
    vec3 pos2 = vec3(modelView * vec4(attribIn[2].position, 1.0));

    vec3 triangleCenter = (pos0 + pos1 + pos2) / 3.0;

    vec3 ambient = mat.emission + lightModelAmbient * mat.ambient;
    vec3 diffuse = vec3(0.0);
    vec3 specular = vec3(0.0);

    // PARTIE ECLARAGE : 
    for (int i = 0; i < 3; i++) {

        vec3 lightPosCam = vec3(view * vec4(lights[i].position, 1.0));
        vec3 L = normalize(lightPosCam - triangleCenter);

        // DIFFUSION
        float diff = max(dot(N, L), 0.0);
        vec3 lightDiffuse_i = lights[i].diffuse * diff;

        // SPECULATION
        float spec = 0.0;
        vec3 V = normalize(-triangleCenter);

        if(useBlinn) {
            vec3 B = normalize(L + V);
            spec = pow(max(dot(N, B), 0.0), mat.shininess);

        } else {
            vec3 R = reflect(-L, N);
            float cosPhi = max(dot(R, V), 0.0);
            spec = pow(cosPhi, mat.shininess);
            
        }

        vec3 lightSpecular_i = mat.specular * lights[i].specular * spec;

        // LUMIERE AMBIANT DE LA LUMIERE EMISE
        vec3 lightAmbient_i = lights[i].ambient * mat.ambient;

        if(useSpotlight) {
            float lightAlignment = dot(-L, lights[i].spotDirection);
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

    // Envoi des résultats aux fragments
    for (int i = 0; i < gl_in.length(); i++) {
        attribOut.texCoords = attribIn[i].texCoords;
        attribOut.emission = mat.emission;
        attribOut.ambient = ambient;
        attribOut.diffuse = diffuse;
        attribOut.specular = specular;
        gl_Position = gl_in[i].gl_Position;
        EmitVertex();
    }
    EndPrimitive();
}
