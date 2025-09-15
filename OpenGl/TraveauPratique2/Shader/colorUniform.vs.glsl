#version 330 core

// TODO

layout(location = 0) in vec3 aPosition;
uniform mat4 matrice;

void main() {
    gl_Position = matrice*vec4(aPosition, 1.0);                     
}
