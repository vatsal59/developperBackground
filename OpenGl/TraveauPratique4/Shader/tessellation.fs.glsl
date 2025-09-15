#version 330 core

in ATTRIB_GS_OUT
{
    float height;
    vec2 texCoords;
    vec4 patchDistance;
    vec3 barycentricCoords;
} attribIn;

uniform sampler2D groundSampler;
uniform sampler2D sandSampler;
uniform sampler2D snowSampler;
uniform bool viewWireframe;

out vec4 FragColor;

float edgeFactor(vec3 barycentricCoords, float width)
{
    vec3 d = fwidth(barycentricCoords);
    vec3 f = step(d * width, barycentricCoords);
    return min(min(f.x, f.y), f.z);
}

float edgeFactor(vec4 barycentricCoords, float width)
{
    vec4 d = fwidth(barycentricCoords);
    vec4 f = step(d * width, barycentricCoords);
    return min(min(min(f.x, f.y), f.z), f.w);
}

const vec3 WIREFRAME_COLOR = vec3(0.5f);
const vec3 PATCH_EDGE_COLOR = vec3(1.0f, 0.0f, 0.0f);

const float WIREFRAME_WIDTH = 0.5f;
const float PATCH_EDGE_WIDTH = 0.5f;

void main()
{
	// TODO
    vec4 texColor;
    vec4 sandColor = texture(sandSampler, attribIn.texCoords);
    vec4 grassColor = texture(groundSampler, attribIn.texCoords);
    vec4 snowColor = texture(snowSampler, attribIn.texCoords);

    float hauteur = attribIn.height;
    if (hauteur <= 0.3)
    {
        texColor = sandColor;
    } else if(hauteur > 0.3 && hauteur <= 0.35)
    {
        texColor = mix(sandColor, grassColor, smoothstep(0.3, 0.35, hauteur));
    } else if(hauteur > 0.35 && hauteur <= 0.6)
    {
        texColor = grassColor;
    } else if(hauteur > 0.6 && hauteur <= 0.65)
    {
         texColor = mix(grassColor, snowColor, smoothstep(0.6, 0.65, hauteur));
    } else
    {
        texColor = snowColor;
    }

     float wireFactor = edgeFactor(attribIn.barycentricCoords, WIREFRAME_WIDTH);
     float patchMixFactor = edgeFactor(attribIn.patchDistance, PATCH_EDGE_WIDTH);

     vec3 finalColor = texColor.rgb;
     finalColor = mix(PATCH_EDGE_COLOR, finalColor, patchMixFactor);

    if (viewWireframe)
    {
        finalColor = mix(WIREFRAME_COLOR, finalColor, wireFactor);
    }

    FragColor = vec4(finalColor, 1.0);
}

    




