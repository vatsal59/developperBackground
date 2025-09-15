#version 330 core

layout(points) in;
layout(triangle_strip, max_vertices = 4) out;


in ATTRIB_VS_OUT
{
    vec4 color;
    vec2 size;
} attribIn[];

out ATTRIB_GS_OUT
{
    vec4 color;
    vec2 texCoords;
} attribOut;

uniform mat4 projection;

void main()
{
    const vec2 quad[4] = vec2[4] (
        vec2(-0.5,  0.5),
        vec2(-0.5, -0.5),
        vec2( 0.5,  0.5),
        vec2( 0.5, -0.5)
    );

    for (int i = 0; i < 4; ++i)
    {
        vec2 offset = attribIn[0].size * quad[i];
        vec4 worldPos = gl_in[0].gl_Position + vec4(offset, 0.0, 0.0);
        gl_Position = projection * worldPos;

        attribOut.color = attribIn[0].color;
        attribOut.texCoords = quad[i] + vec2(0.5);

        EmitVertex();
    }
    EndPrimitive();
}



