#version 400 core

layout(quads) in;

/*
in Attribs {
    vec4 couleur;
} AttribsIn[];*/


out ATTRIB_TES_OUT
{
    float height;
    vec2 texCoords;
    vec4 patchDistance;
} attribOut;

uniform mat4 mvp;

uniform sampler2D heighmapSampler;

vec4 interpole( vec4 v0, vec4 v1, vec4 v2, vec4 v3 )
{
    // mix( x, y, f ) = x * (1-f) + y * f.
    // TODO

    // point que le GPU essaie de generer a linterieur du patch
        float u = gl_TessCoord.x;
        float v = gl_TessCoord.y;
    // on interpole entre honrizontalement et verticalement
        vec4 v0_v1 = mix(v0, v1, u);
        vec4 v2_v3 = mix(v3, v2, u);
    // on retourne la vrai position du point (qui etait dans les coordonner )
        return mix(v0_v1, v2_v3, v);
}


const float PLANE_SIZE = 256.0f;

void main()
{
	// TODO

    vec4 positionEdge = interpole(gl_in[0].gl_Position,
                              gl_in[1].gl_Position,
                              gl_in[2].gl_Position,
                              gl_in[3].gl_Position);

    // on passe de -0.5,0.5 a 0,1
        vec2 texPostion = positionEdge.xz / PLANE_SIZE + vec2(0.5);
        texPostion /= 4.0;
    
    // on lit le niveau de couleurs dans la map de hauteur
        float heightFactor = texture(heighmapSampler , texPostion).r;
        float height = heightFactor*64.0 - 32.0;

    positionEdge.y = height;
    gl_Position = mvp * positionEdge;
    attribOut.patchDistance = vec4(gl_TessCoord.x, 1.0 - gl_TessCoord.x,
                                   gl_TessCoord.y, 1.0 - gl_TessCoord.y);
    attribOut.texCoords = gl_TessCoord.xy * 2.0;
    attribOut.height = heightFactor;

}
