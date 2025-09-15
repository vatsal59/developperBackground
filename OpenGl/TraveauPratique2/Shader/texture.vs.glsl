#version 330 core

// TODO

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 coordTex;
uniform mat4 mvpTexture;
out vec2 texCoord;

void main() {
    gl_Position = mvpTexture*vec4(aPosition, 1.0);   
    texCoord = coordTex;                  
}
