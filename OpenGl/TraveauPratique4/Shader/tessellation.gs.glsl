#version 330 core

layout(triangles) in;
layout(triangle_strip, max_vertices = 3) out;


in ATTRIB_TES_OUT
{
    float height;
    vec2 texCoords;
    vec4 patchDistance;
} attribIn[];

out ATTRIB_GS_OUT
{
    float height;
    vec2 texCoords;
    vec4 patchDistance;
    vec3 barycentricCoords;
} attribOut;

void main()
{
    // TODO
    vec4 p0 = gl_in[0].gl_Position;
    vec4 p1 = gl_in[1].gl_Position;
    vec4 p2 = gl_in[2].gl_Position;
    vec3 barycentric = vec3(0.0);


    attribOut.height = attribIn[0].height;
    attribOut.texCoords = attribIn[0].texCoords;
    attribOut.patchDistance = attribIn[0].patchDistance;
    attribOut.barycentricCoords = vec3(1.0, 0.0, 0.0); 
    gl_Position = p0;
    EmitVertex();

    attribOut.height = attribIn[1].height;
    attribOut.texCoords = attribIn[1].texCoords;
    attribOut.patchDistance = attribIn[1].patchDistance;
    attribOut.barycentricCoords = vec3(0.0, 1.0, 0.0);
    gl_Position = p1;
    EmitVertex();

    attribOut.height = attribIn[2].height;
    attribOut.texCoords = attribIn[2].texCoords;
    attribOut.patchDistance = attribIn[2].patchDistance;
    attribOut.barycentricCoords = vec3(0.0, 0.0, 1.0);
    gl_Position = p2;
    EmitVertex();

    EndPrimitive();
}
