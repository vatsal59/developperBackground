#version 400 core

layout(vertices = 4) out;

uniform mat4 modelView;


const float MIN_TESS = 4;
const float MAX_TESS = 64;

const float MIN_DIST = 30.0f;
const float MAX_DIST = 100.0f;

void main() // si possible mettre tous dans la meme boucle a la fin
{
	// TODO

    gl_in[0].gl_Position; // (0,0)
    gl_in[1].gl_Position; // (1,0)
    gl_in[2].gl_Position; // (1,1)
    gl_in[3].gl_Position; // (0,1)
    // position du sommet pareille
        gl_out[gl_InvocationID].gl_Position = gl_in[gl_InvocationID].gl_Position;
    // referencial de vue
        vec3 vueEdgePosition[4];
        for (int i = 0; i < 4; i++) {
            vueEdgePosition[i] = (modelView * gl_in[i].gl_Position).xyz;
        }
    // calcule des centres des arrets
    vec3 center[4];
    center[0] = (vueEdgePosition[0] + vueEdgePosition[1]) * 0.5; 
    center[1] = (vueEdgePosition[1] + vueEdgePosition[2]) * 0.5; 
    center[2] = (vueEdgePosition[2] + vueEdgePosition[3]) * 0.5;
    center[3] = (vueEdgePosition[3] + vueEdgePosition[0]) * 0.5;  


    float levels[4];
    for (int i = 0; i < 4; i++) {

        float cameraEdgeDist = length(center[i]);
        float factor = clamp((cameraEdgeDist - MIN_DIST) / (MAX_DIST - MIN_DIST), 0.0, 1.0);
        // selon le facteur retourne une grandeur
            levels[i] = mix(MAX_TESS, MIN_TESS, factor);
    }
        gl_TessLevelOuter[0] = levels[3]; 
        gl_TessLevelOuter[1] = levels[0]; 
        gl_TessLevelOuter[2] = levels[1];
        gl_TessLevelOuter[3] = levels[2];

        // tescelation interne
        gl_TessLevelInner[1] = max(levels[1], levels[3]);
        gl_TessLevelInner[0] = max(levels[0], levels[2]);
    
}
